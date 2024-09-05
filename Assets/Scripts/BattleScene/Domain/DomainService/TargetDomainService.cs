using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using Utility.Interface;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.DomainService
{
    public class TargetDomainService
    {
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly EnemiesDomainService _enemies;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly PlayerDomainService _player;
        private readonly IRandomEx _randomEx;

        public TargetDomainService(
            EnemiesDomainService enemies,
            PlayerDomainService player,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRepository<EnemyEntity, CharacterId> enemyRepository,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository,
            IRandomEx randomEx)
        {
            _enemies = enemies;
            _player = player;
            _battleLogRepository = battleLogRepository;
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
            _hitPointRepository = hitPointRepository;
            _randomEx = randomEx;
        }

        public ImmutableList<CharacterId> Get(CharacterId characterId, Range range)
        {
            var targetList = range switch
            {
                Range.Oneself =>
                    _hitPointRepository.Select(characterId).IsSurvive()
                        ? ImmutableList.Create(characterId)
                        : ImmutableList<CharacterId>.Empty,
                Range.Solo =>
                    Equals(characterId, _player.GetId())
                        ? ImmutableList.Create(GetEnemySolo())
                        : ImmutableList.Create(_player.GetId()),
                Range.Line or Range.Random =>
                    Equals(characterId, _player.GetId())
                        ? _enemies.GetIdSurvive()
                        : ImmutableList.Create(_player.GetId()),
                _ => throw new NotImplementedException()
            };

            return targetList;
        }

        private ImmutableList<CharacterId> GetRandom(CharacterId characterId)
        {
            var targetList = _characterRepository.Select()
                .Select(x => x.Id)
                .Where(x => !Equals(x, characterId) && _hitPointRepository.Select(x).IsSurvive())
                .ToList();
            if (targetList.Count == 0) return ImmutableList<CharacterId>.Empty;
            return ImmutableList.Create(_randomEx.Choice(targetList));
        }

        private CharacterId GetEnemySolo()
        {
            var targetId = _battleLogRepository.Select()
                .Where(x => Equals(x.ActorId, _player.GetId()))
                .Where(x => x.TargetIdList.Count == 1)
                .Where(x => x.TargetIdList.Contains(_player.GetId()))
                .Max()?.TargetIdList
                .First();
            targetId = targetId == null || !_hitPointRepository.Select(targetId).IsSurvive()
                ? _enemies.Get()
                    .Select(x => x.Id)
                    .OrderBy(x => _enemyRepository.Select(x).EnemyNumber)
                    .First(x => _hitPointRepository.Select(x).IsSurvive())
                : targetId;
            return targetId;
        }
    }
}
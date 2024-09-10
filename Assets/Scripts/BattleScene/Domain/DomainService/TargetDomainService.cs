using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.DomainService
{
    public class TargetDomainService
    {
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly EnemiesDomainService _enemies;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly PlayerDomainService _player;

        public TargetDomainService(
            EnemiesDomainService enemies,
            PlayerDomainService player,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IRepository<EnemyEntity, CharacterId> enemyRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _enemies = enemies;
            _player = player;
            _battleLogRepository = battleLogRepository;
            _enemyRepository = enemyRepository;
            _characterRepository = characterRepository;
        }

        public ImmutableList<CharacterId> Get(CharacterId characterId, Range range)
        {
            var targetList = range switch
            {
                Range.Oneself =>
                    _characterRepository.Select(characterId).IsSurvive
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

        private CharacterId GetEnemySolo()
        {
            var targetId = _battleLogRepository.Select()
                .Where(x => Equals(x.ActorId, _player.GetId()))
                .Where(x => x.TargetIdList.Count == 1)
                .Where(x => x.TargetIdList.Contains(_player.GetId()))
                .Max()?.TargetIdList
                .First();
            targetId = targetId == null || !_characterRepository.Select(targetId).IsSurvive
                ? _enemies.Get()
                    .Select(x => x.Id)
                    .OrderBy(x => _enemyRepository.Select(x).EnemyNumber)
                    .First(x => _characterRepository.Select(x).IsSurvive)
                : targetId;
            return targetId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Service
{
    public class TargetService : ITargetService
    {
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly EnemiesDomainService _enemies;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly PlayerDomainService _player;
        private readonly IMyRandomService _myRandom;

        public TargetService(
            EnemiesDomainService enemies,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            PlayerDomainService player,
            IMyRandomService myRandom)
        {
            _enemies = enemies;
            _battleLogRepository = battleLogRepository;
            _characterRepository = characterRepository;
            _player = player;
            _myRandom = myRandom;
        }

        public IReadOnlyList<CharacterId> Get(CharacterId actorId, Range range, bool isAutoTarget=false)
        {
            var targetList = range switch
            {
                Range.Oneself =>
                    _characterRepository.Get(actorId).IsSurvive
                        ? new[] { actorId }
                        : Array.Empty<CharacterId>(),
                Range.Solo when isAutoTarget =>
                    _characterRepository.Get(actorId).IsPlayer
                        ? new[] { GetEnemySoloRandom() }
                        : new[] { _player.GetId() },
                Range.Solo when !isAutoTarget =>
                    _characterRepository.Get(actorId).IsPlayer
                        ? new[] { GetEnemySolo() }
                        : new[] { _player.GetId() },
                Range.Line or Range.Random =>
                    _characterRepository.Get(actorId).IsPlayer
                        ? _enemies.GetIdSurvive()
                        : new[] { _player.GetId() },
                _ => throw new NotImplementedException()
            };

            return targetList;
        }

        private CharacterId GetEnemySolo()
        {
            var targetId = _battleLogRepository.Get()
                .Where(x => _characterRepository.Get(x.ActorId)?.IsPlayer ?? false)
                .Where(x => x.TargetIdList.Count == 1)
                .Where(x => !x.TargetIdList.Any(y => _characterRepository.Get(y).IsPlayer))
                .Max()?.TargetIdList
                .Single();
            targetId = targetId == null || !_characterRepository.Get(targetId).IsSurvive
                ? _enemies.Get()
                    .Select(x => x.Id)
                    .OrderBy(x => _characterRepository.Get(x).Position)
                    .First(x => _characterRepository.Get(x).IsSurvive)
                : targetId;
            return targetId;
        }

        private CharacterId GetEnemySoloRandom()
        {
            var enemyIdList = _enemies.Get()
                .Where(x => x.IsSurvive)
                .Select(x => x.Id)
                .ToList();
            var enemyId = _myRandom.Choice(enemyIdList);
            return enemyId;
        }
    }
}
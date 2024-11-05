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
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;
        private readonly EnemiesDomainService _enemies;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly PlayerDomainService _player;
        private readonly IMyRandomService _myRandom;

        public TargetService(
            EnemiesDomainService enemies,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            PlayerDomainService player,
            IMyRandomService myRandom)
        {
            _enemies = enemies;
            _battleLogCollection = battleLogCollection;
            _characterCollection = characterCollection;
            _player = player;
            _myRandom = myRandom;
        }

        public IReadOnlyList<CharacterId> Get(CharacterId actorId, Range range, bool isAutoTarget=false)
        {
            var targetList = range switch
            {
                Range.Oneself =>
                    _characterCollection.Get(actorId).IsSurvive
                        ? new[] { actorId }
                        : Array.Empty<CharacterId>(),
                Range.Solo when isAutoTarget =>
                    _characterCollection.Get(actorId).IsPlayer
                        ? new[] { GetEnemySoloRandom() }
                        : new[] { _player.GetId() },
                Range.Solo when !isAutoTarget =>
                    _characterCollection.Get(actorId).IsPlayer
                        ? new[] { GetEnemySolo() }
                        : new[] { _player.GetId() },
                Range.Line or Range.Random =>
                    _characterCollection.Get(actorId).IsPlayer
                        ? _enemies.GetIdSurvive()
                        : new[] { _player.GetId() },
                _ => throw new NotImplementedException()
            };

            return targetList;
        }

        private CharacterId GetEnemySolo()
        {
            var targetId = _battleLogCollection.Get()
                .Where(x => _characterCollection.Get(x.ActorId)?.IsPlayer ?? false)
                .Where(x => x.TargetIdList.Count == 1)
                .Where(x => !x.TargetIdList.Any(y => _characterCollection.Get(y).IsPlayer))
                .Max()?.TargetIdList
                .Single();
            targetId = targetId == null || !_characterCollection.Get(targetId).IsSurvive
                ? _enemies.Get()
                    .Select(x => x.Id)
                    .OrderBy(x => _characterCollection.Get(x).Position)
                    .First(x => _characterCollection.Get(x).IsSurvive)
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
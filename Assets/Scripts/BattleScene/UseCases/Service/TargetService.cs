using System;
using System.Collections.Generic;
using Utility;
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

        public TargetService(
            EnemiesDomainService enemies,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection,
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _enemies = enemies;
            _battleLogCollection = battleLogCollection;
            _characterCollection = characterCollection;
        }

        public IReadOnlyList<CharacterId> Get(CharacterId actorId, Range range)
        {
            var targetList = range switch
            {
                Range.Oneself =>
                    _characterCollection.Get(actorId).IsSurvive
                        ? new[] { actorId }
                        : MyList<CharacterId>.Empty,
                Range.Solo =>
                    _characterCollection.Get(actorId).IsPlayer
                        ? new[] { GetEnemySolo() }
                        : new[] { actorId },
                Range.Line or Range.Random =>
                    _characterCollection.Get(actorId).IsPlayer
                        ? _enemies.GetIdSurvive()
                        : new[] { actorId },
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
    }
}
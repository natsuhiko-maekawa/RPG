using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.DomainService
{
    public class TargetDomainService
    {
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;
        private readonly EnemiesDomainService _enemies;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly PlayerDomainService _player;

        public TargetDomainService(
            EnemiesDomainService enemies,
            PlayerDomainService player,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection,
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _enemies = enemies;
            _player = player;
            _battleLogCollection = battleLogCollection;
            _characterCollection = characterCollection;
        }

        public IReadOnlyList<CharacterId> Get(CharacterId characterId, Range range)
        {
            var targetList = range switch
            {
                Range.Oneself =>
                    _characterCollection.Get(characterId).IsSurvive
                        ? new[] { characterId }
                        : ImmutableList<CharacterId>.Empty,
                Range.Solo =>
                    Equals(characterId, _player.GetId())
                        ? new[] { GetEnemySolo() }
                        : new[] { _player.GetId() },
                Range.Line or Range.Random =>
                    Equals(characterId, _player.GetId())
                        ? _enemies.GetIdSurvive()
                        : new[] { _player.GetId() },
                _ => throw new NotImplementedException()
            };

            return targetList;
        }

        private CharacterId GetEnemySolo()
        {
            var targetId = _battleLogCollection.Get()
                .Where(x => Equals(x.ActorId, _player.GetId()))
                .Where(x => x.TargetIdList.Count == 1)
                .Where(x => x.TargetIdList.Contains(_player.GetId()))
                .Max()?.TargetIdList
                .First();
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
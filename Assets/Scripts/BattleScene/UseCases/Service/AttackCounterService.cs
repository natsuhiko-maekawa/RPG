using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class AttackCounterService
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;

        public AttackCounterService(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection,
            IFactory<BattlePropertyValueObject> battlePropertyFactory)
        {
            _characterCollection = characterCollection;
            _battleLogCollection = battleLogCollection;
            _battlePropertyFactory = battlePropertyFactory;
        }

        public float GetRate()
        {
            var rate = (float)Count() / _battlePropertyFactory.Create().AttackCountLimit;
            return Math.Min(rate, 1.0f);
        }

        public bool IsOverflow()
        {
            return _battlePropertyFactory.Create().AttackCountLimit < Count();
        }

        private int Count()
        {
            var battleLogList = _battleLogCollection.Get();
            var player = _characterCollection.Get().Single(x => x.IsPlayer);
            var playerId = player.Id;
            var count = battleLogList
                .OrderByDescending(x => x)
                .TakeWhile(x => x.BattleEventCode != BattleEventCode.FatalitySkill)
                .Where(x => Equals(x.ActorId, playerId))
                .Select(x => x.AttackList
                    .Count(y => y.IsHit))
                .Sum();
            return count;
        }
    }
}
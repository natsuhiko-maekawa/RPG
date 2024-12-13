using System;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;

namespace BattleScene.UseCases.Service
{
    public class AttackCounterService
    {
        private readonly IRepository<BattleEventEntity, BattleEventId> _battleLogRepository;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;

        public AttackCounterService(
            IRepository<BattleEventEntity, BattleEventId> battleLogRepository,
            IFactory<BattlePropertyValueObject> battlePropertyFactory)
        {
            _battleLogRepository = battleLogRepository;
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
            var battleLogList = _battleLogRepository.Get();
            var count = battleLogList
                .OrderByDescending(x => x)
                .TakeWhile(x => x.BattleEventCode != BattleEventCode.FatalitySkill)
                .Where(x => x.Actor is { IsPlayer: true })
                .Select(x => x.AttackList
                    .Count(y => y.IsHit))
                .Sum();
            return count;
        }
    }
}
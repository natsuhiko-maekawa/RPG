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
using System;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using Utility;

namespace BattleScene.UseCases.Services
{
    public class SlipDamageRegistererService
    {
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IRepository<BattleEventEntity, BattleEventId> _battleLogRepository;
        private readonly BattleLoggerService _battleLogger;

        public SlipDamageRegistererService(
            DamageEvaluatorService damageEvaluator,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IRepository<BattleEventEntity, BattleEventId> battleLogRepository,
            BattleLoggerService battleLogger)
        {
            _damageEvaluator = damageEvaluator;
            _battlePropertyFactory = battlePropertyFactory;
            _battleLogRepository = battleLogRepository;
            _battleLogger = battleLogger;
        }

        public void Register(SlipCode slipCode)
        {
            // 現在のスリップコードから直近に罹ったスリップのログを取得する
            // TODO: 稀にNullReferenceExceptionが発生する。
            var slipEvent = _battleLogRepository.Get()
                .Where(x => x.BattleEventCode is BattleEventCode.Skill or BattleEventCode.FatalitySkill)
                .Where(x => x.SlipCode == slipCode)
                .Where(x => !x.IsFailure)
                .Max();
            if (slipEvent is null)
            {
                MyDebug.Log($"SlipCode: {slipCode}");
            }

            var actor = slipEvent.Actor ?? throw new InvalidOperationException();
            var targetList = slipEvent.TargetList;
            var target = targetList.Single();

            var slipDamageEvent = _battleLogger.GetLast();

            var damageRate = _battlePropertyFactory.Create().SlipDefaultDamageRate;
            var damageParameter = new DamageValueObject(
                damageRate: damageRate,
                damageExpressionCode: DamageExpressionCode.Slip);

            var attack = new AttackValueObject(
                amount: _damageEvaluator.Evaluate(actor, target, damageParameter),
                target: target,
                isHit: true,
                attacksWeakPoint: false,
                index: 0);

            slipDamageEvent.UpdateSlipDamage(
                attackList: new[] { attack },
                targetList: targetList);
        }
    }
}
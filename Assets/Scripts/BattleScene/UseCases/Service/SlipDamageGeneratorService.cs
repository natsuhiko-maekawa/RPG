using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class SlipDamageGeneratorService
    {
        private readonly PlayerDomainService _player;
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;

        public SlipDamageGeneratorService(
            PlayerDomainService player,
            DamageEvaluatorService damageEvaluator,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository)
        {
            _player = player;
            _damageEvaluator = damageEvaluator;
            _battlePropertyFactory = battlePropertyFactory;
            _battleLogRepository = battleLogRepository;
        }

        public BattleEventValueObject Generate(SlipCode slipCode)
        {
            // 現在のスリップコードから直近に罹ったスリップのログを取得する
            // TODO: 稀にNullReferenceExceptionが発生する
            var battleLog = _battleLogRepository.Get()
                .Where(x => x.BattleEventCode is BattleEventCode.Skill or BattleEventCode.FatalitySkill)
                .Where(x => x.SlipCode == slipCode)
                .Where(x => !x.IsFailure)
                .Max();
            var actorId = battleLog.ActorId;
            if (actorId == null) throw new InvalidOperationException();

            var targetId = _player.GetId();

            var damageRate = _battlePropertyFactory.Create().SlipDefaultDamageRate;
            var damageParameter = new DamageValueObject(
                DamageRate: damageRate,
                DamageExpressionCode: DamageExpressionCode.Slip);

            var attack = new AttackValueObject(
                amount: _damageEvaluator.Evaluate(actorId, targetId, damageParameter),
                targetId: targetId,
                isHit: true,
                attacksWeakPoint: false,
                index: 0);

            var slipDamage = BattleEventValueObject.CreateSlipDamage(
                targetIdList: new List<CharacterId> { targetId },
                slipCode: slipCode,
                attackList: new List<AttackValueObject> { attack });
            return slipDamage;
        }
    }
}
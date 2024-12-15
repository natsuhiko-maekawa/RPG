using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DomainServices;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;
using static BattleScene.Domain.Codes.Range;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     混乱
    /// </summary>
    public class ConfusionSkill : BaseSkill
    {
        private readonly OrderItemsDomainService _orderItems;

        public ConfusionSkill(OrderItemsDomainService orderItems)
        {
            _orderItems = orderItems;
        }

        public override SkillCode SkillCode { get; } = SkillCode.Confusion;
        public override Range Range { get; } = Oneself;
        public override MessageCode AttackMessageCode => GetAttackMessageCode();

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new AlwaysHitDamage() };

        private MessageCode GetAttackMessageCode()
        {
            if (!_orderItems.First().TryGetActor(out var actor)) throw new InvalidOperationException();
            var attackMessageCode = actor.IsPlayer
                ? MessageCode.PlayerConfusionActMessage
                : MessageCode.EnemyConfusionActMessage;
            return attackMessageCode;
        }
    }
}
using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.InterfaceAdapter.Skills.BaseClass;
using static BattleScene.Domain.Code.Range;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skills
{
    /// <summary>
    ///     混乱
    /// </summary>
    public class ConfusionSkill : BaseSkill
    {
        private readonly OrderedItemsDomainService _orderedItems;

        public ConfusionSkill(OrderedItemsDomainService orderedItems)
        {
            _orderedItems = orderedItems;
        }

        public override SkillCode SkillCode { get; } = SkillCode.Confusion;
        public override Range Range { get; } = Oneself;
        public override MessageCode AttackMessageCode => GetAttackMessageCode();

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new AlwaysHitDamage() };

        private MessageCode GetAttackMessageCode()
        {
            if (!_orderedItems.First().TryGetActor(out var actor)) throw new InvalidOperationException();
            var attackMessageCode = actor.IsPlayer
                ? MessageCode.PlayerConfusionActMessage
                : MessageCode.EnemyConfusionActMessage;
            return attackMessageCode;
        }
    }
}
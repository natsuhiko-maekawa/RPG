using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.InterfaceAdapter.Skills.BaseClass;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skills
{
    /// <summary>
    ///     鏡花水月
    /// </summary>
    public class KyoukasuigetsuSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Kyoukasuigetsu;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override Range Range { get; } = Range.Line;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NoMessage;

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new[] { new AbsoluteConfusion() };

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };
    }
}
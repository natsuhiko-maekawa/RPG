using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     鏡花水月
    /// </summary>
    public class KyoukasuigetsuSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Kyoukasuigetsu;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override Range Range { get; } = Range.Line;
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new[] { new AbsoluteConfusion() };

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };
    }
}
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     スモークボム
    /// </summary>
    public class SmokeBombSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.SmokeBomb;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Line;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new[] { new EnemyBlind() };
    }
}
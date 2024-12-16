using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     レーション
    /// </summary>
    public class FieldRationSkill : BaseSkill
    {
        public FieldRationSkill(BasicCure basicCure)
        {
            CureList = new[] { basicCure };
        }

        public override SkillCode SkillCode { get; } = SkillCode.FieldRation;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseCure> CureList { get; }
    }
}
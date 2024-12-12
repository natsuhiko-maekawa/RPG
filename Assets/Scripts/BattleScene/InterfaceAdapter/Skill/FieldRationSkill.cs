using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
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
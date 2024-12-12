using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skills.BaseClass;

namespace BattleScene.InterfaceAdapter.Skills
{
    public class ParalysisSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Paralysis;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.ParalysisCantActMessage;
    }
}
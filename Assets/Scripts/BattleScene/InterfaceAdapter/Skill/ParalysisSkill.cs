using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    public class ParalysisSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Paralysis;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.ParalysisCantActMessage;
    }
}
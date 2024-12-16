using BattleScene.Domain.Codes;
using BattleScene.Presenters.Skills.BaseClass;

namespace BattleScene.Presenters.Skills
{
    public class ParalysisSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Paralysis;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.ParalysisCantActMessage;
    }
}
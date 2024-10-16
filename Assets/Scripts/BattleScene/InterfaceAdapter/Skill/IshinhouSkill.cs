using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     医心方
    /// </summary>
    public class IshinhouSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Ishinhou;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.ResetAilmentMessage;

        public override ImmutableList<BaseReset> ResetList { get; }
            = ImmutableList.Create<BaseReset>(new Ishinhou());
    }
}
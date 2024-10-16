using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     スモークボム
    /// </summary>
    public class SmokeBombSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.SmokeBomb;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Line;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<BaseAilment> AilmentList { get; }
            = ImmutableList.Create<BaseAilment>(new EnemyBlind());
    }
}
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     スモークボム
    /// </summary>
    internal class SmokeBombSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.SmokeBomb;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Line;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Gun;
        public override MessageCode Description { get; } = MessageCode.SmokeBombDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<AbstractAilment> AilmentList { get; }
            = ImmutableList.Create<AbstractAilment>(new Blind());
    }
}
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     撫斬り
    /// </summary>
    internal class NadegiriSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Nadegiri;
        public override int TechnicalPoint { get; } = 7;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override Range Range { get; } = Range.Line;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.NadegiriDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new Nadegiri());
    }
}
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     ランダムショット
    /// </summary>
    internal class RandomShotsSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.RandomShots;
        public override int TechnicalPoint { get; } = 15;
        public override Range Range { get; } = Range.Random;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Gun;
        public override MessageCode Description { get; } = MessageCode.RandomShotDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new RandomShot());
    }
}
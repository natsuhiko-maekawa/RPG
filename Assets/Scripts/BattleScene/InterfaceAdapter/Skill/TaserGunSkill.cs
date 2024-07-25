using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     テーザーガン
    /// </summary>
    internal class TaserGunSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.TaserGun;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Solo;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Gun;
        public override MessageCode Description { get; } = MessageCode.TaserGunDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());

        public override ImmutableList<AbstractAilment> AilmentList { get; }
            = ImmutableList.Create<AbstractAilment>(new EnemyParalysis());
    }
}
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    internal class AttackSkill : AbstractSkill
    {
        public AttackSkill(BasicDamage basicDamage)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
        }

        public override SkillCode SkillCode { get; } = SkillCode.Attack;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; } =
            ImmutableList.Create<AbstractDamage>(new BasicDamage());
    }
}
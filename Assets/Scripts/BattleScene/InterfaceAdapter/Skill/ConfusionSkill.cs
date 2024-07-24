using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;
using static BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     混乱
    /// </summary>
    internal class ConfusionSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Confusion;
        public override Range Range { get; } = Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Confusion;
        public override MessageCode AttackMessageCode { get; } = MessageCode.ConfusionActMessage;
        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new AlwaysHitDamage());
    }
}
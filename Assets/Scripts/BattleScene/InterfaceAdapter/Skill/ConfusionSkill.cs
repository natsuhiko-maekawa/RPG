using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using static BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     混乱
    /// </summary>
    internal class ConfusionSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Confusion;
        public override Range Range { get; } = Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.ConfusionActMessage;
        public override ImmutableList<BaseDamage> DamageList { get; }
            = ImmutableList.Create<BaseDamage>(new AlwaysHitDamage());
    }
}
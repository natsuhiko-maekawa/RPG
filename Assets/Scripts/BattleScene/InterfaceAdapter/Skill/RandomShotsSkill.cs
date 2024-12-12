using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     ランダムショット
    /// </summary>
    public class RandomShotsSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.RandomShots;
        public override int TechnicalPoint { get; } = 15;
        public override Range Range { get; } = Range.Random;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new RandomShot() };
    }
}
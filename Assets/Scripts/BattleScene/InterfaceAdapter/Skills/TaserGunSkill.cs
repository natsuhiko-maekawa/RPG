using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.InterfaceAdapter.Skills.BaseClass;

namespace BattleScene.InterfaceAdapter.Skills
{
    /// <summary>
    ///     テーザーガン
    /// </summary>
    public class TaserGunSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.TaserGun;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Solo;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new[] { new EnemyParalysis() };
    }
}
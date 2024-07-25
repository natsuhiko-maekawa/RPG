using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     レーション
    /// </summary>
    internal class FieldRationSkill : AbstractSkill
    {
        public FieldRationSkill(BasicCure basicCure)
        {
            CureList = ImmutableList.Create<AbstractCure>(basicCure);
        }

        public override SkillCode SkillCode { get; } = SkillCode.FieldRation;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Gun;
        public override MessageCode Description { get; } = MessageCode.FieldRationDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.RestoreHitPointMessage;

        public override ImmutableList<AbstractCure> CureList { get; }
    }
}
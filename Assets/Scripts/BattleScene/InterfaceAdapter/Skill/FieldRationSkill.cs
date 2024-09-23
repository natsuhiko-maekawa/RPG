using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     レーション
    /// </summary>
    internal class FieldRationSkill : BaseSkill
    {
        public FieldRationSkill(BasicCure basicCure)
        {
            CureList = ImmutableList.Create<BaseCure>(basicCure);
        }

        public override SkillCode SkillCode { get; } = SkillCode.FieldRation;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Gun;
        public override MessageCode Description { get; } = MessageCode.FieldRationDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.RestoreHitPointMessage;

        public override ImmutableList<BaseCure> CureList { get; }
    }
}
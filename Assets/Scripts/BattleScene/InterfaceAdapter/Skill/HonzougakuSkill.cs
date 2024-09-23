using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     本草学
    /// </summary>
    internal class HonzougakuSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Honzougaku;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.HonzougakuDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.RemoveAilmentMessage;

        public override ImmutableList<BaseReset> ResetList { get; } 
            = ImmutableList.Create<BaseReset>(new Honzougaku());
    }
}
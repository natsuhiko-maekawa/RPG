using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     ファーストエイド
    /// </summary>
    internal class FirstAidSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.FirstAid;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Gun;
        public override MessageCode Description { get; } = MessageCode.FirstAidDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.RecoverDestroyedPartMessage;

        public override ImmutableList<BaseReset> ResetList { get; }
            = ImmutableList.Create<BaseReset>(new FirstAid());
    }
}
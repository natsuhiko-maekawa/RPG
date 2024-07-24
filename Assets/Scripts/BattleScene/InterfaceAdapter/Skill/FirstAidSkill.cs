using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     ファーストエイド
    /// </summary>
    internal class FirstAidSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.FirstAid;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Gun;
        public override MessageCode Description { get; } = MessageCode.FirstAidDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.RecoverDestroyedPartMessage;

        public override ImmutableList<AbstractReset> ResetList { get; }
            = ImmutableList.Create<AbstractReset>(new FirstAid());
    }
}
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     防御
    /// </summary>
    internal class DefenceSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Defence;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Defence;
        public override MessageCode AttackMessageCode { get; } = MessageCode.DefenceMessage;

        public override ImmutableList<AbstractBuff> BuffList { get; }
            = ImmutableList.Create<AbstractBuff>(new Defence());
        // TODO: TPを回復するスキルをAddする

    }
}
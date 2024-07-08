using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     本草学
    /// </summary>
    internal class HonzougakuSkill : AbstractSkill
    {
        private readonly Honzougaku _honzougaku;

        public HonzougakuSkill(Honzougaku honzougaku)
        {
            ResetSkillElementList = ImmutableList.Create<AbstractReset>(honzougaku);
        }

        public override int GetTechnicalPoint()
        {
            return 3;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Katana;
        }

        public override Range GetRange()
        {
            return Range.Oneself;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.HonzougakuDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.RemoveAilmentsMessage;
        }
    }
}
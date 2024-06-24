using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     防御
    /// </summary>
    public class DefenceSkill : AbstractSkill
    {
        private readonly DefenceSkillElement _defenceSkillElement;

        public DefenceSkill(DefenceSkillElement defenceSkillElement)
        {
            _defenceSkillElement = defenceSkillElement;
        }

        public override Range GetRange()
        {
            return Range.Oneself;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Defence;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.DefenceDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.DefenceMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            // TODO: TPを回復するスキルをAddする
            return ImmutableList.Create<ISkillElement>(_defenceSkillElement);
        }
    }
}
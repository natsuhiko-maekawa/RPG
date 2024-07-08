using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     防御
    /// </summary>
    internal class DefenceSkill : AbstractSkill
    {
        public DefenceSkill(DefenceSkillElement defenceSkillElement)
        {
            BuffSkillElementList = ImmutableList.Create<BuffSkillElement>(defenceSkillElement);
            // TODO: TPを回復するスキルをAddする
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
    }
}
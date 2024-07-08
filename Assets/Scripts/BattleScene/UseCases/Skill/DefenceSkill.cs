using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     防御
    /// </summary>
    internal class DefenceSkill : AbstractSkill
    {
        public DefenceSkill(Defence defence)
        {
            BuffList = ImmutableList.Create<AbstractBuff>(defence);
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
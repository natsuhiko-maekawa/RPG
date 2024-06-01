using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;
using static BattleScene.Domain.Code.Range;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     混乱
    /// </summary>
    internal class ConfusionSkill : AbstractSkill
    {
        private readonly AlwaysHitDamageSkillElement _alwaysHitDamageSkillElement;

        public ConfusionSkill(AlwaysHitDamageSkillElement alwaysHitDamageSkillElement)
        {
            _alwaysHitDamageSkillElement = alwaysHitDamageSkillElement;
        }

        public override Range GetRange()
        {
            return Oneself;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Confusion;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.ConfusionActMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_alwaysHitDamageSkillElement);
        }
    }
}
using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    internal class AttackSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;

        public AttackSkill(BasicDamageSkillElement basicDamageSkillElement)
        {
            _basicDamageSkillElement = basicDamageSkillElement;
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.AttackMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_basicDamageSkillElement);
        }
    }
}
using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     侘助
    /// </summary>
    internal class WabisukeSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;
        private readonly WabisukeSkillElement _wabisukeSkillElement;

        public WabisukeSkill(BasicDamageSkillElement basicDamageSkillElement, WabisukeSkillElement wabisukeSkillElement)
        {
            _basicDamageSkillElement = basicDamageSkillElement;
            _wabisukeSkillElement = wabisukeSkillElement;
        }

        public override int GetTechnicalPoint()
        {
            return 10;
        }

        public override ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList.Create(BodyPartCode.Arm);
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Katana;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.WabisukeDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.AttackMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_basicDamageSkillElement, _wabisukeSkillElement);
        }
    }
}
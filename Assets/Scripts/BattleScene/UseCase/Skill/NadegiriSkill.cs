using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     撫斬り
    /// </summary>
    internal class NadegiriSkill : AbstractSkill
    {
        private readonly NadegiriSkillElement _nadegiriSkillElement;

        public override int GetTechnicalPoint()
        {
            return 7;
        }

        public override ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList.Create(BodyPartCode.Arm);
        }

        public override Range GetRange()
        {
            return Range.Line;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Katana;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.NadegiriDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.AttackMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_nadegiriSkillElement);
        }
    }
}
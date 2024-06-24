using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using Utility;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     命を刈る鎌
    /// </summary>
    internal class PutScytheSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;
        private readonly RandomEx _randomEx;

        public PutScytheSkill(BasicDamageSkillElement basicDamageSkillElement, RandomEx randomEx)
        {
            _basicDamageSkillElement = basicDamageSkillElement;
            _randomEx = randomEx;
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList.Create(BodyPartCode.Arm);
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Damaged;
        }

        public override MessageCode GetAttackMessage()
        {
            return _randomEx.Choice(
                new[] { MessageCode.CutArmMessage, MessageCode.CutLegMessage, MessageCode.CutStomachMessage });
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_basicDamageSkillElement);
        }
    }
}
using System;
using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;
using static BattleScene.Domain.Code.PlayerImageCode;
using static BattleScene.Domain.Code.MessageCode;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     鏡花水月
    /// </summary>
    internal class KyoukasuigetsuSkill : AbstractSkill
    {
        private readonly AbsoluteConfusionSkillElement _absoluteConfusionSkillElement;
        private readonly BasicDamageSkillElement _basicDamageSkillElement;

        public KyoukasuigetsuSkill(
            AbsoluteConfusionSkillElement absoluteConfusionSkillElement,
            BasicDamageSkillElement basicDamageSkillElement)
        {
            _absoluteConfusionSkillElement = absoluteConfusionSkillElement;
            _basicDamageSkillElement = basicDamageSkillElement;
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
            return Katana;
        }

        public override MessageCode GetDescription()
        {
            return KyoukasuigetsuDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            throw new NotImplementedException();
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_basicDamageSkillElement, _absoluteConfusionSkillElement);
        }
    }
}
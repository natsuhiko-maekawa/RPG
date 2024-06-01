using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;
using static BattleScene.Domain.Code.MessageCode;
using Random = UnityEngine.Random;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     切り刻み
    /// </summary>
    internal class CutUpSkill : AbstractSkill
    {
        private readonly FiveTimeDamageSkillElement _fiveTimeDamageSkillElement;

        private readonly int _rand = Random.Range(0, 2);

        public CutUpSkill(FiveTimeDamageSkillElement fiveTimeDamageSkillElement)
        {
            _fiveTimeDamageSkillElement = fiveTimeDamageSkillElement;
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
            return PlayerImageCode.Damaged;
        }

        public override MessageCode GetAttackMessage()
        {
            return new[] { CutUpArmMessage, CutUpLegMessage, CutUpStomachMessage }[_rand];
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_fiveTimeDamageSkillElement);
        }
    }
}
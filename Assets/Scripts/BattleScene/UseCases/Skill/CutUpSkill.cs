using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using static BattleScene.Domain.Code.MessageCode;
using Random = UnityEngine.Random;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     切り刻み
    /// </summary>
    internal class CutUpSkill : AbstractSkill
    {
        private readonly int _rand = Random.Range(0, 2);

        public CutUpSkill(FiveTimeDamage fiveTimeDamage)
        {
            DamageSkillElementList = ImmutableList.Create<AbstractDamage>(fiveTimeDamage);
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
    }
}
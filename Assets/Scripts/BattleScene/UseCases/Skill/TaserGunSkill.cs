using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     テーザーガン
    /// </summary>
    internal class TaserGunSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;
        private readonly EnemyParalysisSkillElement _enemyParalysisSkillElement;

        public TaserGunSkill(
            BasicDamageSkillElement basicDamageSkillElement,
            EnemyParalysisSkillElement enemyParalysisSkillElement)
        {
            DamageSkillElementList = ImmutableList.Create<DamageSkillElement>(basicDamageSkillElement);
            AilmentSkillElementList = ImmutableList.Create<AilmentSkillElement>(enemyParalysisSkillElement);
        }

        public override int GetTechnicalPoint()
        {
            return 5;
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
            return PlayerImageCode.Gun;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.TaserGunDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.AttackMessage;
        }
    }
}
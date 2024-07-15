using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     テーザーガン
    /// </summary>
    internal class TaserGunSkill : AbstractSkill
    {
        private readonly BasicDamage _basicDamage;
        private readonly EnemyParalysis _enemyParalysis;

        public TaserGunSkill(
            BasicDamage basicDamage,
            EnemyParalysis enemyParalysis)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
            AilmentList = ImmutableList.Create<AbstractAilment>(enemyParalysis);
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
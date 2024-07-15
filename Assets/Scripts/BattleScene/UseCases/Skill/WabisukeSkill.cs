using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     侘助
    /// </summary>
    internal class WabisukeSkill : AbstractSkill
    {
        private readonly BasicDamage _basicDamage;
        private readonly Wabisuke _wabisuke;

        public WabisukeSkill(
            BasicDamage basicDamage, 
            Wabisuke wabisuke)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
            BuffList = ImmutableList.Create<AbstractBuff>(wabisuke);
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
    }
}
using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class PlayerPropertyValueObject
    {
        public PlayerPropertyValueObject(
            int technicalPoint,
            ImmutableList<SkillCode> fatalitySkills)
        {
            TechnicalPoint = technicalPoint;
            FatalitySkills = fatalitySkills;
        }

        public int TechnicalPoint { get; }
        public ImmutableList<SkillCode> FatalitySkills { get; }
    }
}
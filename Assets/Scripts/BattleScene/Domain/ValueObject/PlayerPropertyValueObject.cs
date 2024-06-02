using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class PlayerPropertyValueObject
    {
        public PlayerPropertyValueObject(
            int technicalPoint,
            SkillCode[] fatalitySkills)
        {
            TechnicalPoint = technicalPoint;
            FatalitySkills = ImmutableList.Create(fatalitySkills);
        }

        public int TechnicalPoint { get; }
        public ImmutableList<SkillCode> FatalitySkills { get; }
    }
}
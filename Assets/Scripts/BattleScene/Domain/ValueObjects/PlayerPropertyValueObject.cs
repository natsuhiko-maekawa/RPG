using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public class PlayerPropertyValueObject
    {
        public PlayerPropertyValueObject(
            int technicalPoint,
            SkillCode[] fatalitySkillList)
        {
            TechnicalPoint = technicalPoint;
            FatalitySkillList = fatalitySkillList;
        }

        public int TechnicalPoint { get; }
        public SkillCode[] FatalitySkillList { get; }
    }
}
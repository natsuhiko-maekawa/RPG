using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class BodyPartViewInfoValueObject
    {
        public BodyPartCode BodyPartCode { get; }
        public string BodyPartName { get; }

        public BodyPartViewInfoValueObject(
            BodyPartCode bodyPartCode, 
            string bodyPartName)
        {
            BodyPartCode = bodyPartCode;
            BodyPartName = bodyPartName;
        }
    }
}
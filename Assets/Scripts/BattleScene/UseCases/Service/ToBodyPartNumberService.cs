using BattleScene.Domain.Code;

namespace BattleScene.UseCases.Service
{
    public class ToBodyPartNumberService
    {
        public int BodyPart(BodyPartCode bodyPartCode)
        {
            return (int)bodyPartCode;
        }
    }
}
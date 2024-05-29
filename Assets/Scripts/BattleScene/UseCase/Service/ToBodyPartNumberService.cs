using BattleScene.Domain.Code;

namespace BattleScene.UseCase.Service
{
    public class ToBodyPartNumberService
    {
        public int BodyPart(BodyPartCode bodyPartCode)
        {
            return (int)bodyPartCode;
        }
    }
}
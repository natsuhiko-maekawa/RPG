using BattleScene.Domain.Id;

namespace BattleScene.UseCase.BusinessLogic.Interface
{
    public interface IBusinessLogic
    {
        public void Execute(FrameNumber nextFrameNumber);
    }
}
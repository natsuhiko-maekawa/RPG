using BattleScene.UseCase.EventRunner;

namespace BattleScene.UseCase.Event.Interface
{
    internal interface IEvent
    {
        public EventCode Run();
    }
}
using BattleScene.UseCase.Event.Runner;

namespace BattleScene.UseCase.Event.Interface
{
    internal interface IEvent
    {
        public EventCode Run();
    }
}
using BattleScene.UseCases.Event.Runner;

namespace BattleScene.UseCases.Event.Interface
{
    internal interface IEvent
    {
        public EventCode Run();
    }
}
using BattleScene.UseCases.Event.Interface;

namespace BattleScene.UseCases.Event.Runner
{
    internal interface IEventFactory
    {
        public IEvent Create(EventCode eventCode);
    }
}
using BattleScene.UseCase.Event.Interface;

namespace BattleScene.UseCase.Event.Runner
{
    internal interface IEventFactory
    {
        public IEvent Create(EventCode eventCode);
    }
}
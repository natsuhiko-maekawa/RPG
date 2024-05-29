using BattleScene.UseCase.Event.Interface;

namespace BattleScene.UseCase.EventRunner
{
    internal interface IEventFactory
    {
        public IEvent Create(EventCode eventCode);
    }
}
using BattleScene.UseCases.OldEvent.Interface;

namespace BattleScene.UseCases.OldEvent.Runner
{
    internal interface IEventFactory
    {
        public IOldEvent Create(EventCode eventCode);
    }
}
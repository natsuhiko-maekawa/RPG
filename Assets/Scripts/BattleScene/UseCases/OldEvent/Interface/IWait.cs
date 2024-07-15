using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent.Interface
{
    internal interface IWait
    {
        public EventCode NextEvent();
    }
}
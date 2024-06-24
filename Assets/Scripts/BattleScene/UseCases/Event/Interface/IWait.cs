using BattleScene.UseCases.Event.Runner;

namespace BattleScene.UseCases.Event.Interface
{
    internal interface IWait
    {
        public EventCode NextEvent();
    }
}
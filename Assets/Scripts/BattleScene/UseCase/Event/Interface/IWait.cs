using BattleScene.UseCase.Event.Runner;

namespace BattleScene.UseCase.Event.Interface
{
    internal interface IWait
    {
        public EventCode NextEvent();
    }
}
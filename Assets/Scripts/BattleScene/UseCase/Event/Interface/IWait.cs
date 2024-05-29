using BattleScene.UseCase.EventRunner;

namespace BattleScene.UseCase.Event.Interface
{
    internal interface IWait
    {
        public EventCode NextEvent();
    }
}
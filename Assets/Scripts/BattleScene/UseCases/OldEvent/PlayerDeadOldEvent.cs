using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class PlayerDeadOldEvent : IOldEvent, IWait
    {
        public EventCode Run()
        {
            // メッセージを表示
            // var playerDeadMessage = _messageOutputDataFactory.Create(PlayerDeadMessage);
            // _messageViewPresenter.Start(playerDeadMessage);
            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return IsContinueEvent;
        }
    }
}
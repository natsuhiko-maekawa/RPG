using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class PlayerWinOldEvent : IOldEvent, IWait
    {
        public EventCode Run()
        {
            // メッセージを表示
            // var playerWinMessage = _messageOutputDataFactory.Create(PlayerWinMessage);
            // _messageViewPresenter.Start(playerWinMessage);
            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return IsContinueEvent;
        }
    }
}
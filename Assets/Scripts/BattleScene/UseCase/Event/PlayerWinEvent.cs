using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.EventRunner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Event
{
    internal class PlayerWinEvent : IEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;
        
        public EventCode Run()
        {
            var playerWinMessage = _messageOutputDataFactory.Create(PlayerWinMessage);
            _messageViewPresenter.Start(playerWinMessage);
            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.IsContinueEvent;
        }
    }
}
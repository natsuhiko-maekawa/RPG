using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class PlayerWinOldEvent : IOldEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;

        public PlayerWinOldEvent(
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageViewPresenter)
        {
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageViewPresenter = messageViewPresenter;
        }

        public EventCode Run()
        {
            var playerWinMessage = _messageOutputDataFactory.Create(PlayerWinMessage);
            _messageViewPresenter.Start(playerWinMessage);
            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return IsContinueEvent;
        }
    }
}
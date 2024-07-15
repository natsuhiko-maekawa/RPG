using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class PlayerDeadOldEvent : IOldEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;

        public PlayerDeadOldEvent(
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageViewPresenter)
        {
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageViewPresenter = messageViewPresenter;
        }

        public EventCode Run()
        {
            var playerDeadMessage = _messageOutputDataFactory.Create(PlayerDeadMessage);
            _messageViewPresenter.Start(playerDeadMessage);
            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.IsContinueEvent;
        }
    }
}
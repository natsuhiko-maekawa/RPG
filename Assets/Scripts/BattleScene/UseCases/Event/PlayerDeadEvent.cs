using BattleScene.UseCases.Event.Interface;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.Event.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.Event
{
    internal class PlayerDeadEvent : IEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;

        public PlayerDeadEvent(
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
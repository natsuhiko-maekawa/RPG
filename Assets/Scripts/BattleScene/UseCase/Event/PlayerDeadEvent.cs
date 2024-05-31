using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.Event.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Event
{
    internal class PlayerDeadEvent : IEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;

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
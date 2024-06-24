using BattleScene.Domain.Code;
using BattleScene.UseCases.Event.Interface;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;

namespace BattleScene.UseCases.Event
{
    public class AilmentSkillFailureViewEvent : IEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;

        public AilmentSkillFailureViewEvent(
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView)
        {
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
        }

        public EventCode Run()
        {
            var failAilmentsMessageOutputData = _messageOutputDataFactory.Create(MessageCode.FailAilmentsMessage);
            _messageView.Start(failAilmentsMessageOutputData);
            return EventCode.WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}
using BattleScene.Domain.Code;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;

namespace BattleScene.UseCases.OldEvent
{
    public class AilmentSkillFailureViewOldEvent : IOldEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;

        public AilmentSkillFailureViewOldEvent(
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView)
        {
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
        }

        public EventCode Run()
        {
            var failAilmentsMessageOutputData = _messageOutputDataFactory.Create(MessageCode.FailAilmentMessage);
            _messageView.Start(failAilmentsMessageOutputData);
            return EventCode.WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}
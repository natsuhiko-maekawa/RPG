﻿using BattleScene.Domain.Code;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;

namespace BattleScene.UseCase.Event
{
    public class AilmentSkillFailureViewEvent : IEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        
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
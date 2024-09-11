﻿using BattleScene.Domain.Code;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentMessageState : AbstractSkillState
    {
        private readonly IMessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public AilmentMessageState(
            IMessageViewPresenter messageView,
            SkillEndState skillEndState)
        {
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _messageView.Start(MessageCode.AilmentMessage);
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}
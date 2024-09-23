﻿using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class SlipMessageState : BaseState<SlipParameterValueObject, SlipValueObject>
    {
        private readonly MessageViewPresenter _messageView;
        private readonly PrimeSkillStopState<SlipParameterValueObject, SlipValueObject> _primeSkillStopState;

        public SlipMessageState(
            MessageViewPresenter messageView,
            PrimeSkillStopState<SlipParameterValueObject, SlipValueObject> primeSkillStopState)
        {
            _messageView = messageView;
            _primeSkillStopState = primeSkillStopState;
        }

        public override void Start()
        {
            _messageView.StartMessageAnimationAsync(MessageCode.AilmentMessage);
        }
        
        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}
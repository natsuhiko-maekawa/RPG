using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class RestoreMessageState : BaseState<RestoreParameterValueObject, RestoreValueObject>
    {
        private readonly MessageViewPresenter _messageView;
        private readonly PrimeSkillStopState<RestoreParameterValueObject, RestoreValueObject> _primeSkillStopState;

        public RestoreMessageState(
            MessageViewPresenter messageView,
            PrimeSkillStopState<RestoreParameterValueObject, RestoreValueObject> primeSkillStopState)
        {
            _messageView = messageView;
            _primeSkillStopState = primeSkillStopState;
        }

        public override void Start()
        {
            _messageView.StartMessageAnimationAsync(MessageCode.RestoreTechnicalPointMessage);
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}
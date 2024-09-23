using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentMessageState : PrimeSkillOutputState<AilmentParameterValueObject, AilmentValueObject>
    {
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly PrimeSkillStopState<AilmentParameterValueObject, AilmentValueObject> _primeSkillStopState;

        public AilmentMessageState(
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            PrimeSkillStopState<AilmentParameterValueObject, AilmentValueObject> primeSkillStopState)
        {
            _messageView = messageView;
            _playerImageView = playerImageView;
            _primeSkillStopState = primeSkillStopState;
        }

        public override void Start()
        {
            _messageView.StartMessageAnimationAsync(MessageCode.AilmentMessage);
            _playerImageView.StartAnimationAsync(PlayerImageCode.Suffocation);
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_primeSkillStopState);
        }
    }
}
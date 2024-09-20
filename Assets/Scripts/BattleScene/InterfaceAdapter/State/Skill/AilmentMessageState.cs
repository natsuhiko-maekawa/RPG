using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentMessageState : AbstractSkillState
    {
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly SkillEndState _skillEndState;

        public AilmentMessageState(
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            SkillEndState skillEndState)
        {
            _messageView = messageView;
            _playerImageView = playerImageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _messageView.Start(MessageCode.AilmentMessage);
            _playerImageView.StartAnimationAsync(PlayerImageCode.Suffocation);
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}
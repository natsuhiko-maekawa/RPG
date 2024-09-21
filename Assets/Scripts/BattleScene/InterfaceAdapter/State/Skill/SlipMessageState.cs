using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SlipMessageState : AbstractSkillState
    {
        private readonly MessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public SlipMessageState(
            MessageViewPresenter messageView,
            SkillEndState skillEndState)
        {
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _messageView.StartMessageAnimationAsync(MessageCode.AilmentMessage);
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}
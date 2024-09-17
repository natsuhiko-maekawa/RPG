using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentMessageState : AbstractSkillState
    {
        private readonly MessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public AilmentMessageState(
            MessageViewPresenter messageView,
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
using BattleScene.Domain.Code;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class RestoreMessageState : AbstractSkillState
    {
        private readonly IMessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public RestoreMessageState(
            IMessageViewPresenter messageView,
            SkillEndState skillEndState)
        {
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _messageView.Start(MessageCode.RestoreTechnicalPointMessage);
        }

        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}
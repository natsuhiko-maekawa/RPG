using BattleScene.Domain.Code;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageMessageState : AbstractSkillState
    {
        private readonly IMessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public DamageMessageState(
            IMessageViewPresenter messageView,
            SkillEndState skillEndState)
        {
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _messageView.Start(MessageCode.DamageMessage);
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}
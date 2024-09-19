using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentFailureState : AbstractSkillState
    {
        private readonly MessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public AilmentFailureState(
            MessageViewPresenter messageView,
            SkillEndState skillEndState)
        {
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _messageView.Start(MessageCode.FailAilmentMessage);
        }
        
        public override void Select()
        {
            // TODO: 要修正
            // TODO: 本当はダメージスキルだけをスキップしたい
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}
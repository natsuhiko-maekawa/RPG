using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentFailureState : AbstractSkillState
    {
        private readonly MessageViewPresenter _messageView;
        private readonly SkillQuitState _skillQuitState;

        public AilmentFailureState(
            MessageViewPresenter messageView,
            SkillQuitState skillQuitState)
        {
            _messageView = messageView;
            _skillQuitState = skillQuitState;
        }

        public override void Start()
        {
            _messageView.Start(MessageCode.FailAilmentMessage);
        }
        
        public override void Select()
        {
            // TODO: 要修正
            // TODO: 本当はダメージスキルだけをスキップしたい
            SkillContext.TransitionTo(_skillQuitState);
        }
    }
}
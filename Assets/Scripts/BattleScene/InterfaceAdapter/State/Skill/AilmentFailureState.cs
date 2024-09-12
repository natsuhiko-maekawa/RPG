using BattleScene.Domain.Code;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentFailureState : AbstractSkillState
    {
        private readonly IMessageViewPresenter _messageView;
        private readonly SkillQuitState _skillQuitState;

        public AilmentFailureState(
            IMessageViewPresenter messageView,
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
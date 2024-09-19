using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageMessageState : AbstractSkillState
    {
        private readonly DamageDigitOutputDataFactory _damageDigitOutputDataFactory;
        private readonly IDigitViewPresenter _digitView;
        private readonly MessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public DamageMessageState(
            DamageDigitOutputDataFactory damageDigitOutputDataFactory,
            IDigitViewPresenter digitView,
            MessageViewPresenter messageView, 
            SkillEndState skillEndState)
        {
            _damageDigitOutputDataFactory = damageDigitOutputDataFactory;
            _digitView = digitView;
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _messageView.Start(MessageCode.DamageMessage);
            _digitView.Start(_damageDigitOutputDataFactory.Create());
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}
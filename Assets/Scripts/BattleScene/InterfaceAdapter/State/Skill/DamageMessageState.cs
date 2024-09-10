using BattleScene.Domain.Code;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageMessageState : AbstractSkillState
    {
        private readonly DamageDigitOutputDataFactory _damageDigitOutputDataFactory;
        private readonly IDigitViewPresenter _digitView;
        private readonly IMessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public DamageMessageState(
            DamageDigitOutputDataFactory damageDigitOutputDataFactory, 
            IDigitViewPresenter digitView, 
            IMessageViewPresenter messageView, 
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
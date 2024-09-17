using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;
using BattleScene.UseCases.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCases.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageMessageState : AbstractSkillState
    {
        private readonly DamageDigitOutputDataFactory _damageDigitOutputDataFactory;
        private readonly HitPointBarOutputDataFactory _hitPointBarOutputDataFactory;
        private readonly IDigitViewPresenter _digitView;
        private readonly IHitPointBarViewPresenter _hitPointBarView;
        private readonly MessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public DamageMessageState(
            DamageDigitOutputDataFactory damageDigitOutputDataFactory, 
            HitPointBarOutputDataFactory hitPointBarOutputDataFactory,
            IDigitViewPresenter digitView, 
            IHitPointBarViewPresenter hitPointBarView,
            MessageViewPresenter messageView, 
            SkillEndState skillEndState)
        {
            _damageDigitOutputDataFactory = damageDigitOutputDataFactory;
            _hitPointBarOutputDataFactory = hitPointBarOutputDataFactory;
            _digitView = digitView;
            _hitPointBarView = hitPointBarView;
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _messageView.Start(MessageCode.DamageMessage);
            _digitView.Start(_damageDigitOutputDataFactory.Create());
            _hitPointBarView.Start(_hitPointBarOutputDataFactory.Create());
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}
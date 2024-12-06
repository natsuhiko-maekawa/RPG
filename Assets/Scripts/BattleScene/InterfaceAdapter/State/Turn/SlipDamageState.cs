using System;
using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.UseCases.UseCase;
using R3;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class SlipDamageState : BaseState
    {
        private readonly SlipUseCase _useCase;
        private readonly SlipDamagePresenterFacade _facade;
        private readonly TurnStopState _turnStopState;
        private ActionQueue _actionQueue = null!;
        private readonly Action<Unit> _transitionStateAction;

        public SlipDamageState(
            SlipUseCase useCase,
            SlipDamagePresenterFacade facade,
            TurnStopState turnStopState)
        {
            _useCase = useCase;
            _facade = facade;
            _turnStopState = turnStopState;
            _transitionStateAction = _ => TransitionState();
        }

        public override void Start()
        {
            Context.Skill = _useCase.GetSkillCode(Context.SlipCode);
            Context.TargetIdList = _useCase.GetTargetList();
            _useCase.RegisterBattleEvent(Context.SlipCode);

            var outputQueue = _facade.GetOutputQueue(Context.Skill);
            _actionQueue = new ActionQueue(outputQueue);
            _actionQueue.OnLastAction.Subscribe(_transitionStateAction);
            _actionQueue.Invoke();
        }

        public override void Select()
        {
            _actionQueue.Invoke();
        }

        private void TransitionState()
        {
            // 複数メソッドにまたがるため、手動でDisposeせざるを得ない
            _actionQueue.Dispose();
            Context.TransitionTo(_turnStopState);
        }
    }
}
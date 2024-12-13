using System;
using BattleScene.Presenters.PresenterFacades;
using BattleScene.UseCases.UseCases;
using R3;

namespace BattleScene.Presenters.States.Turn
{
    public class SlipDamageState : BaseState
    {
        private readonly SlipUseCase _useCase;
        private readonly SlipDamagePresenterFacade _facade;
        private readonly CharacterDeadState _characterDeadState;
        private readonly TurnStopState _turnStopState;
        private ActionQueue _actionQueue;
        private readonly Action<Unit> _transitionStateAction;

        public SlipDamageState(
            SlipUseCase useCase,
            SlipDamagePresenterFacade facade,
            CharacterDeadState characterDeadState,
            TurnStopState turnStopState)
        {
            _useCase = useCase;
            _facade = facade;
            _characterDeadState = characterDeadState;
            _turnStopState = turnStopState;
            _transitionStateAction = _ => TransitionState();
        }

        public override void Start()
        {
            Context.Skill = _useCase.GetSkillCode(Context.SlipCode);
            Context.TargetList = _useCase.GetTargetList();
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
            // QUESTION: インスタンスの利用が複数メソッドにまたがる場合、手動でDisposeしてもよいか。
            _actionQueue.Dispose();
            BaseState nextState;
            if (_useCase.IsPlayerDeadInThisTurn())
            {
                nextState = _characterDeadState;
            }
            else
            {
                nextState = _turnStopState;
                Context.NextStateCode = StateCode.TurnState;
            }
            Context.TransitionTo(nextState);
        }
    }
}
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class DamageOutputState : PrimeSkillOutputState<DamageValueObject>
    {
        private readonly DamageOutputFacade _damageOutput;
        private readonly PrimeSkillStopState<DamageValueObject> _primeSkillStopState;
        private readonly PrimeSkillBreakState<DamageValueObject> _primeSkillBreakState;

        public DamageOutputState(
            DamageOutputFacade damageOutput,
            PrimeSkillStopState<DamageValueObject> primeSkillStopState,
            PrimeSkillBreakState<DamageValueObject> primeSkillBreakState)
        {
            _damageOutput = damageOutput;
            _primeSkillStopState = primeSkillStopState;
            _primeSkillBreakState = primeSkillBreakState;
        }

        public override async void Start()
        {
            await _damageOutput.Output(Context.BattleEventQueue.Peek());
        }

        public override void Select()
        {
            BaseState<DamageValueObject> nextState = Context.BattleEventQueue.Peek().IsAvoid
                ? _primeSkillBreakState
                : _primeSkillStopState;
            Context.TransitionTo(nextState);
        }
    }
}
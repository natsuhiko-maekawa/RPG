using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class DamageOutputState : PrimeSkillOutputState<DamageParameterValueObject>
    {
        private readonly DamageOutputFacade _damageOutput;
        private readonly PrimeSkillStopState<DamageParameterValueObject> _primeSkillStopState;
        private readonly PrimeSkillBreakState<DamageParameterValueObject> _primeSkillBreakState;

        public DamageOutputState(
            DamageOutputFacade damageOutput,
            PrimeSkillStopState<DamageParameterValueObject> primeSkillStopState,
            PrimeSkillBreakState<DamageParameterValueObject> primeSkillBreakState)
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
            BaseState<DamageParameterValueObject> nextState = Context.BattleEventQueue.Peek().IsAvoid
                ? _primeSkillBreakState
                : _primeSkillStopState;
            Context.TransitionTo(nextState);
        }
    }
}
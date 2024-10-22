using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class DamageOutputState : PrimeSkillOutputState<DamageParameterValueObject, PrimeSkillValueObject>
    {
        private readonly DamageOutputFacade _damageOutput;
        private readonly PrimeSkillStopState<DamageParameterValueObject, PrimeSkillValueObject> _primeSkillStopState;
        private readonly PrimeSkillBreakState<DamageParameterValueObject, PrimeSkillValueObject> _primeSkillBreakState;

        public DamageOutputState(
            DamageOutputFacade damageOutput,
            PrimeSkillStopState<DamageParameterValueObject, PrimeSkillValueObject> primeSkillStopState,
            PrimeSkillBreakState<DamageParameterValueObject, PrimeSkillValueObject> primeSkillBreakState)
        {
            _damageOutput = damageOutput;
            _primeSkillStopState = primeSkillStopState;
            _primeSkillBreakState = primeSkillBreakState;
        }

        public override async void Start()
        {
            await _damageOutput.Output(Context.PrimeSkillQueue.Peek());
        }

        public override void Select()
        {
            BaseState<DamageParameterValueObject, PrimeSkillValueObject> nextState = Context.PrimeSkillQueue.Peek().IsAvoid
                ? _primeSkillBreakState
                : _primeSkillStopState;
            Context.TransitionTo(nextState);
        }
    }
}
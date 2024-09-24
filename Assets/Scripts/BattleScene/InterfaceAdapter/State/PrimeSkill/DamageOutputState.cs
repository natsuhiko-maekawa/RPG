using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class DamageOutputState : PrimeSkillOutputState<DamageParameterValueObject, DamageValueObject>
    {
        private readonly DamageOutputFacade _damageOutput;
        private readonly PrimeSkillStopState<DamageParameterValueObject, DamageValueObject> _primeSkillStopState;
        private readonly PrimeSkillBreakState<DamageParameterValueObject, DamageValueObject> _primeSkillBreakState;

        public DamageOutputState(
            DamageOutputFacade damageOutput,
            PrimeSkillStopState<DamageParameterValueObject, DamageValueObject> primeSkillStopState,
            PrimeSkillBreakState<DamageParameterValueObject, DamageValueObject> primeSkillBreakState)
        {
            _damageOutput = damageOutput;
            _primeSkillStopState = primeSkillStopState;
            _primeSkillBreakState = primeSkillBreakState;
        }

        public override async void Start()
        {
            await _damageOutput.Output();
        }
        
        public override void Select()
        {
            BaseState<DamageParameterValueObject, DamageValueObject> nextState = Context.PrimeSkillQueue.Peek().IsAvoid
                ? _primeSkillBreakState
                : _primeSkillStopState;
            Context.TransitionTo(nextState);
        }
    }
}
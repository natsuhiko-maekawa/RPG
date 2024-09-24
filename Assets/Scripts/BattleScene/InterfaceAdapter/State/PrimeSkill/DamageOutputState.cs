using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class DamageOutputState : PrimeSkillOutputState<DamageParameterValueObject, DamageValueObject>
    {
        private readonly DamageOutputFacade _damageOutput;
        private readonly PrimeSkillStopState<DamageParameterValueObject, DamageValueObject> _primeSkillStopState;

        public DamageOutputState(
            DamageOutputFacade damageOutput,
            PrimeSkillStopState<DamageParameterValueObject, DamageValueObject> primeSkillStopState)
        {
            _damageOutput = damageOutput;
            _primeSkillStopState = primeSkillStopState;
        }

        public override async void Start()
        {
            await _damageOutput.Output();
        }
        
        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}
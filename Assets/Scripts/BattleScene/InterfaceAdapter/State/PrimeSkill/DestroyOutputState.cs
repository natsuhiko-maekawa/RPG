using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class DestroyOutputState : PrimeSkillOutputState<DestroyParameterValueObject, DestroyValueObject>
    {
        private readonly PrimeSkillStopState<DestroyParameterValueObject, DestroyValueObject> _primeSkillStopState;

        public DestroyOutputState(
            PrimeSkillStopState<DestroyParameterValueObject, DestroyValueObject> primeSkillStopState)
        {
            _primeSkillStopState = primeSkillStopState;
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}
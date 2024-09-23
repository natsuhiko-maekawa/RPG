using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DestroyState : AbstractSkillState<DestroyedParameterValueObject, DestroyedPartValueObject>
    {
        private readonly PrimeSkillStopState<DestroyedParameterValueObject, DestroyedPartValueObject> _primeSkillStopState;

        public DestroyState(
            PrimeSkillStopState<DestroyedParameterValueObject, DestroyedPartValueObject> primeSkillStopState)
        {
            _primeSkillStopState = primeSkillStopState;
        }

        public override void Start()
        {
            SkillContext.TransitionTo(_primeSkillStopState);
        }
    }
}
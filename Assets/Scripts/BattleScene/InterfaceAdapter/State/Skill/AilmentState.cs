using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Dto;
using BattleScene.UseCases.Interface;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentState : AbstractSkillState
    {
        private readonly PrimeSkillParameterDto<AilmentParameterValueObject> _ailmentParameterDto;
        private readonly IPrimeSkill<AilmentParameterValueObject, AilmentValueObject> _ailment;
        private readonly AilmentParameterValueObject _ailmentParameter;
        private readonly AilmentMessageState _ailmentMessageState;
        private readonly AilmentFailureState _ailmentFailureState;

        public AilmentState(
            PrimeSkillParameterDto<AilmentParameterValueObject> ailmentParameterDto,
            IPrimeSkill<AilmentParameterValueObject, AilmentValueObject> ailment,
            AilmentMessageState ailmentMessageState,
            AilmentFailureState ailmentFailureState)
        {
            _ailmentParameterDto = ailmentParameterDto;
            _ailment = ailment;
            _ailmentMessageState = ailmentMessageState;
            _ailmentFailureState = ailmentFailureState;
        }

        public override void Start()
        {
            var ailmentList = _ailment.Commit(_ailmentParameterDto);
            var failure = ailmentList
                .All(x => x.ActualTargetIdList.Count == 0);
            AbstractSkillState nextState = failure
                ? _ailmentFailureState 
                : _ailmentMessageState;
            SkillContext.TransitionTo(nextState);
        }
    }
}
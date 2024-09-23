using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Dto;
using BattleScene.UseCases.Interface;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentStateFactory
    {
        private readonly IPrimeSkill<AilmentParameterValueObject, AilmentValueObject> _ailment;
        private readonly AilmentMessageState _ailmentMessageState;
        private readonly AilmentFailureState _ailmentFailureState;

        public AilmentStateFactory(
            IPrimeSkill<AilmentParameterValueObject, AilmentValueObject> ailment,
            AilmentMessageState ailmentMessageState,
            AilmentFailureState ailmentFailureState)
        {
            _ailment = ailment;
            _ailmentMessageState = ailmentMessageState;
            _ailmentFailureState = ailmentFailureState;
        }

        public AilmentState Create(
            PrimeSkillParameterDto<AilmentParameterValueObject> ailmentParameterDto)
        {
            return new AilmentState(
                ailmentParameterDto: ailmentParameterDto,
                ailment: _ailment,
                ailmentMessageState: _ailmentMessageState,
                ailmentFailureState: _ailmentFailureState);
        }
    }
}
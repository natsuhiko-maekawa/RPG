using BattleScene.UseCases.Dto;
using BattleScene.UseCases.Interface;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class CommitPrimeSkillState<TPrimeSkillParameterValueObject, TPrimeSkillValueObject> : AbstractSkillState
    {
        private readonly PrimeSkillParameterDto<TPrimeSkillParameterValueObject> _primeSkillParameterDto;
        private readonly IPrimeSkill<TPrimeSkillParameterValueObject, TPrimeSkillValueObject> _primeSkill;
        private readonly OutputPrimeSkillStateFactory<TPrimeSkillValueObject> _outputPrimeSkillStateFactory;

        public CommitPrimeSkillState(
            IPrimeSkill<TPrimeSkillParameterValueObject, TPrimeSkillValueObject> primeSkill,
            OutputPrimeSkillStateFactory<TPrimeSkillValueObject> outputPrimeSkillStateFactory)
        {
            _primeSkill = primeSkill;
            _outputPrimeSkillStateFactory = outputPrimeSkillStateFactory;
        }

        public override void Start()
        {
            var primeSkillList = _primeSkill.Commit(_primeSkillParameterDto);
            var outputPrimeSkillState = _outputPrimeSkillStateFactory.Create(primeSkillList);
            SkillContext.TransitionTo(outputPrimeSkillState);
        }
    }
}
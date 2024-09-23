using System.Collections.Generic;
using BattleScene.UseCases.Interface;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class CommitPrimeSkillState<TPrimeSkillValueObject> : AbstractSkillState
    {
        private readonly IReadOnlyList<TPrimeSkillValueObject> _primeSkillList;
        private readonly IPrimeSkillCollection<TPrimeSkillValueObject> _primeSkillCollection;
        private readonly OutputPrimeSkillStateFactory<TPrimeSkillValueObject> _outputPrimeSkillStateFactory;

        public CommitPrimeSkillState(
            IPrimeSkillCollection<TPrimeSkillValueObject> primeSkillCollection,
            OutputPrimeSkillStateFactory<TPrimeSkillValueObject> outputPrimeSkillStateFactory)
        {
            _primeSkillCollection = primeSkillCollection;
            _outputPrimeSkillStateFactory = outputPrimeSkillStateFactory;
        }

        public override void Start()
        {
            _primeSkillCollection.Commit(_primeSkillList);
            var outputPrimeSkillState = _outputPrimeSkillStateFactory.Create(_primeSkillList);
            SkillContext.TransitionTo(outputPrimeSkillState);
        }
    }
}
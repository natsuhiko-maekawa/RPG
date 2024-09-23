using BattleScene.UseCases.Interface;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class PrimeSkillStartState<TPrimeSkillParameter, TPrimeSkill>
        : AbstractSkillState<TPrimeSkillParameter, TPrimeSkill>
    {
        private readonly IPrimeSkill<TPrimeSkillParameter, TPrimeSkill> _primeSkill;
        private readonly TPrimeSkillParameter _primeSkillParameter;
        private readonly PrimeSkillOutputState<TPrimeSkillParameter, TPrimeSkill> _primeSkillOutputState;

        public PrimeSkillStartState(
            IPrimeSkill<TPrimeSkillParameter, TPrimeSkill> primeSkill,
            PrimeSkillOutputState<TPrimeSkillParameter, TPrimeSkill> primeSkillOutputState)
        {
            _primeSkill = primeSkill;
            _primeSkillOutputState = primeSkillOutputState;
        }

        public override void Start()
        {
            SkillContext.PrimeSkillList = _primeSkill.Commit(
                skillCommon: SkillContext.SkillCommon,
                primeSkillParameter: SkillContext.PrimeSkillParameterList,
                targetIdList: SkillContext.TargetIdList);
            SkillContext.TransitionTo(_primeSkillOutputState);
        }
    }
}
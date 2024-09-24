using BattleScene.UseCases.Interface;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class PrimeSkillStartState<TPrimeSkillParameter, TPrimeSkill>
        : BaseState<TPrimeSkillParameter, TPrimeSkill>
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
            Context.PrimeSkillList = _primeSkill.Commit(
                skillCommon: Context.SkillCommon,
                primeSkillParameterList: Context.PrimeSkillParameterList,
                targetIdList: Context.TargetIdList);
            Context.TransitionTo(_primeSkillOutputState);
        }
    }
}
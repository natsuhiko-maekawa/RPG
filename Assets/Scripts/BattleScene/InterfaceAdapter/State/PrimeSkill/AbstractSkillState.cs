namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public abstract class AbstractSkillState<TPrimeSkillParameter, TPrimeSkill>
    {
        protected SkillContext<TPrimeSkillParameter, TPrimeSkill> SkillContext { get; set; }

        // ReSharper disable once ParameterHidesMember
        public void SetContext(SkillContext<TPrimeSkillParameter, TPrimeSkill> skillContext)
        {
            SkillContext = skillContext;
        }
        
        public virtual void Start()
        {
        }
        
        public virtual void Select()
        {
        }
    }
}
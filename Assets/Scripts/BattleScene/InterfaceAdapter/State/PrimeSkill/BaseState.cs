namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public abstract class BaseState<TPrimeSkillParameter, TPrimeSkill>
    {
        protected Context<TPrimeSkillParameter, TPrimeSkill> Context { get; set; }

        // ReSharper disable once ParameterHidesMember
        public void SetContext(Context<TPrimeSkillParameter, TPrimeSkill> context)
        {
            Context = context;
        }
        
        public virtual void Start()
        {
        }
        
        public virtual void Select()
        {
        }
    }
}
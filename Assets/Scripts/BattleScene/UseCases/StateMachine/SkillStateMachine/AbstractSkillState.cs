namespace BattleScene.UseCases.StateMachine.SkillStateMachine
{
    public abstract class AbstractSkillState
    {
        protected SkillContext SkillContext { get; set; }

        // ReSharper disable once ParameterHidesMember
        public void SetContext(SkillContext skillContext)
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
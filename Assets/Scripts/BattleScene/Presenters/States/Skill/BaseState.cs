namespace BattleScene.Presenters.States.Skill
{
    public abstract class BaseState<TSkillComponent>
    {
        protected Context<TSkillComponent> Context { get; private set; } = null!;

        public void SetContext(Context<TSkillComponent> context)
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
namespace BattleScene.Presenters.States.Skill
{
    public abstract class BaseState<TSkillElement>
    {
        protected Context<TSkillElement> Context { get; private set; } = null!;

        public void SetContext(Context<TSkillElement> context)
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
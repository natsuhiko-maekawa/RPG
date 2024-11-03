namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public abstract class BaseState<TPrimeSkillParameter>
    {
        protected Context<TPrimeSkillParameter> Context { get; private set; } = null!;

        public void SetContext(Context<TPrimeSkillParameter> context)
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
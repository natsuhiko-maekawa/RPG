namespace BattleScene.UseCases.StateMachine.SkillStateMachine
{
    public class SkillContext
    {
        private AbstractSkillState _skillState;

        public SkillContext(AbstractSkillState skillState)
        {
            TransitionTo(skillState);
        }

        public void TransitionTo(AbstractSkillState skillState)
        {
            _skillState = skillState;
            _skillState.SetContext(this);
            _skillState.Start();
        }

        public void Select() => _skillState.Select();
    }
}
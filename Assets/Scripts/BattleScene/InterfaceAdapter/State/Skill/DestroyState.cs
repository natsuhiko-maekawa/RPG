namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DestroyState : AbstractSkillState
    {
        private readonly SkillEndState _skillEndState;

        public DestroyState(
            SkillEndState skillEndState)
        {
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}
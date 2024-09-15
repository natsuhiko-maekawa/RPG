using UnityEngine;

namespace BattleScene.InterfaceAdapter.State.Skill
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
            Debug.Log(skillState.GetType().Name);
            _skillState = skillState;
            _skillState.SetContext(this);
            _skillState.Start();
        }

        public void Select() => _skillState.Select();

        public bool HasEndState() => _skillState is SkillEndState;
    }
}
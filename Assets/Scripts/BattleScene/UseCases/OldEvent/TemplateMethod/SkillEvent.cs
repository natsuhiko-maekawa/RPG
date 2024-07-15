using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent.TemplateMethod
{
    public abstract class SkillEvent
    {
        public EventCode Run()
        {
            UpdateResultRepository();
            UpdateSkillRepository();
            return RunSkillEvent();
        }

        protected abstract void UpdateResultRepository();
        protected abstract void UpdateSkillRepository();
        protected abstract EventCode RunSkillEvent();
    }
}
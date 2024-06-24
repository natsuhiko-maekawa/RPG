using BattleScene.UseCases.Event.Runner;

namespace BattleScene.UseCases.Event.TemplateMethod
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
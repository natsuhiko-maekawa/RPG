using BattleScene.UseCase.Event.Runner;

namespace BattleScene.UseCase.Event.TemplateMethod
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
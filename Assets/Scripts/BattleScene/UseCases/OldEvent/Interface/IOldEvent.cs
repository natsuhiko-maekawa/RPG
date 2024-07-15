using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent.Interface
{
    internal interface IOldEvent
    {
        public EventCode Run();
    }
}
using BattleScene.UseCases.StateMachine;

namespace BattleScene.UseCases.Event
{
    public abstract class BaseEvent
    {
        public StateCode Execute()
        {
            UseCase();
            Output();
            return StateCode;
        }

        public abstract void UseCase();
        public abstract void Output();
        public abstract StateCode StateCode { get; set; }
    }
}
using BattleScene.UseCases.StateMachine;

namespace BattleScene.UseCases.Event
{
    public abstract class BaseEvent
    {
        public StateCode Execute()
        {
            UseCase();
            Output();
            return GetStateCode();
        }

        public abstract void UseCase();
        public abstract void Output();
        public abstract StateCode GetStateCode();
    }
}
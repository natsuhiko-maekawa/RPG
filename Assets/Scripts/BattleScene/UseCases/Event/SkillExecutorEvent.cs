using BattleScene.UseCases.StateMachine;

namespace BattleScene.UseCases.Event
{
    internal class SkillExecutorEvent : BaseEvent
    {
        private readonly SkillIterator _skillIterator;
        private StateCode _stateCode;
        
        public override void UseCase()
        {
            _stateCode = _skillIterator.TryExecuteNext(out var stateCode)
                ? stateCode
                : StateCode.LoopEnd;
        }

        public override void Output()
        {
        }

        public override StateCode GetStateCode()
        {
            return _stateCode;
        }
    }
}
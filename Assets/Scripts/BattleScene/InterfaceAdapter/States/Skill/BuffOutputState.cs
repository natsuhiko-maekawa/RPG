using System;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacades;

namespace BattleScene.InterfaceAdapter.States.Skill
{
    public class BuffOutputState : SkillElementOutputState<BuffValueObject>
    {
        private readonly BuffOutputPresenterFacade _facade;
        private readonly SkillElementStopState<BuffValueObject> _skillElementStopState;

        public BuffOutputState(
            SkillElementStopState<BuffValueObject> skillElementStopState,
            BuffOutputPresenterFacade facade)
        {
            _skillElementStopState = skillElementStopState;
            _facade = facade;
        }

        public override void Start()
        {
            if (Context.BattleEventQueue.Count == 0)
                throw new InvalidOperationException(ExceptionMessage.ContextBattleEventQueueIsEmpty);
            var buffEvent = Context.BattleEventQueue.Dequeue();
            _facade.Output(buffEvent);
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}
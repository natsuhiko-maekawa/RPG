using System;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
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
            var buff = Context.BattleEventQueue.Dequeue();
            _facade.Output(buff);
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}
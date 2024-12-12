using System;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class EnhanceOutputState: SkillElementOutputState<EnhanceValueObject>
    {
        private readonly EnhanceOutputPresenterFacade _facade;
        private readonly SkillElementStopState<EnhanceValueObject> _skillElementStopState;

        public EnhanceOutputState(
            EnhanceOutputPresenterFacade facade,
            SkillElementStopState<EnhanceValueObject> skillElementStopState)
        {
            _facade = facade;
            _skillElementStopState = skillElementStopState;
        }

        public override void Start()
        {
            if (Context.BattleEventQueue.Count == 0)
                throw new InvalidOperationException(ExceptionMessage.ContextBattleEventQueueIsEmpty);
            var enhanceEvent = Context.BattleEventQueue.Dequeue();
            _facade.Output(enhanceEvent);
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}
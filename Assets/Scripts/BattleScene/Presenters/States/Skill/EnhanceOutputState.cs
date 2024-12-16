using System;
using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
{
    public class EnhanceOutputState : SkillOutputState<EnhanceValueObject>
    {
        private readonly EnhanceOutputPresenterFacade _facade;
        private readonly SkillStopState<EnhanceValueObject> _skillStopState;

        public EnhanceOutputState(
            EnhanceOutputPresenterFacade facade,
            SkillStopState<EnhanceValueObject> skillStopState)
        {
            _facade = facade;
            _skillStopState = skillStopState;
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
            Context.TransitionTo(_skillStopState);
        }
    }
}
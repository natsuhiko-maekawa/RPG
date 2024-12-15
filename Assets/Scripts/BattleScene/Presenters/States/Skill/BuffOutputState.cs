using System;
using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
{
    public class BuffOutputState : SkillOutputState<BuffValueObject>
    {
        private readonly BuffOutputPresenterFacade _facade;
        private readonly SkillStopState<BuffValueObject> _skillStopState;

        public BuffOutputState(
            SkillStopState<BuffValueObject> skillStopState,
            BuffOutputPresenterFacade facade)
        {
            _skillStopState = skillStopState;
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
            Context.TransitionTo(_skillStopState);
        }
    }
}
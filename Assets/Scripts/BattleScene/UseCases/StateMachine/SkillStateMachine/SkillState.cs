using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.UseCases.StateMachine.SkillStateMachine;
using VContainer;

namespace BattleScene.UseCases.StateMachine.SkillStack
{
    public class SkillState : AbstractState
    {
        private readonly IObjectResolver _container;
        private readonly SkillCode _skillCode;
        private readonly Queue<SkillContext> _skillContextQueue;

        public SkillState(
            SkillCode skillCode,
            IObjectResolver container)
        {
            _skillCode = skillCode;
            _container = container;
        }

        public override void Select()
        {
            if (_skillContextQueue.TryDequeue(out var skillContext))
            {
                skillContext.Select();
                Context.TransitionTo(this);
            }
            
            Context.TransitionTo(_container.Resolve<TurnEndState>());
        }
    }
}
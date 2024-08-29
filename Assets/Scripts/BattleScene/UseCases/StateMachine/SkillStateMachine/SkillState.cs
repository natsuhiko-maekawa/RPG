using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using VContainer;

namespace BattleScene.UseCases.StateMachine.SkillStateMachine
{
    public class SkillState : AbstractState
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly SkillCode _skillCode;
        private Queue<SkillContext> _skillContextQueue;

        public SkillState(
            SkillCode skillCode,
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _skillCode = skillCode;
            _container = container;
            _skillFactory = skillFactory;
        }

        public override void Start()
        {
            SetSkillContextQueue();
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

        private void SetSkillContextQueue()
        {
            var skill = _skillFactory.Create(_skillCode);
            var skillStates = Enumerable.Empty<AbstractSkillState>()
                .Concat(skill.AilmentList.Select(x => new AilmentState(x)))
                .Concat(skill.DamageList.Select(x => new DamageState(x)));
            var skillContexts = skillStates
                .Select(x => new SkillContext(x));
            _skillContextQueue = new Queue<SkillContext>(skillContexts);
        }
    }
}
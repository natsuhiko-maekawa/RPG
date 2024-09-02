using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.State.Battle;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SkillState : AbstractState
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly BuffStateFactory _buffStateFactory;
        private readonly DamageStateFactory _damageStateFactory;
        private readonly SkillCode _skillCode;
        private Queue<SkillContext> _skillContextQueue;

        public SkillState(
            SkillCode skillCode,
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            BuffStateFactory buffStateFactory,
            DamageStateFactory damageStateFactory)
        {
            _skillCode = skillCode;
            _container = container;
            _skillFactory = skillFactory;
            _buffStateFactory = buffStateFactory;
            _damageStateFactory = damageStateFactory;
        }

        public override void Start()
        {
            SetSkillContextQueue();
        }

        public override void Select()
        {
            var skillContext = _skillContextQueue.Peek();
            skillContext.Select();
            if (skillContext.HasEndState() && _skillContextQueue.Count <= 1)
            {
                Context.TransitionTo(_container.Resolve<TurnEndState>());
                return;
            }

            _skillContextQueue.Dequeue();
        }

        private void SetSkillContextQueue()
        {
            var skill = _skillFactory.Create(_skillCode);
            var skillStates = Enumerable.Empty<AbstractSkillState>()
                .Concat(skill.AilmentList.Select(x => new AilmentState(x)))
                .Concat(skill.DamageList.Select(x => _damageStateFactory.Create(skill.SkillCommon, x)))
                .Concat(skill.BuffList.Select(x => _buffStateFactory.Create(skill.SkillCommon, x)));
            var skillContexts = skillStates
                .Select(x => new SkillContext(x));
            _skillContextQueue = new Queue<SkillContext>(skillContexts);
        }
    }
}
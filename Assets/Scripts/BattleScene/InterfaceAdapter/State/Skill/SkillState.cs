using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.State.Battle;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using UnityEngine;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SkillState : AbstractState
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly BuffStateFactory _buffStateFactory;
        private readonly DamageStateFactory _damageStateFactory;
        private readonly RestoreStateFactory _restoreStateFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly SkillCode _skillCode;
        private Queue<AbstractSkillState> _skillStateQueue;
        private SkillContext _skillContext;

        public SkillState(
            SkillCode skillCode,
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            BuffStateFactory buffStateFactory,
            DamageStateFactory damageStateFactory,
            RestoreStateFactory restoreStateFactory,
            IMessageViewPresenter messageView)
        {
            _skillCode = skillCode;
            _container = container;
            _skillFactory = skillFactory;
            _buffStateFactory = buffStateFactory;
            _damageStateFactory = damageStateFactory;
            _restoreStateFactory = restoreStateFactory;
            _messageView = messageView;
        }

        public override void Start()
        {
            SetSkillContextQueue();
            var skill = _skillFactory.Create(_skillCode);
            _messageView.Start(skill.SkillCommon.MessageCode);
        }

        public override void Select()
        {
            Debug.Assert(_skillStateQueue.Count > 0);
            
            if (_skillContext == null || _skillContext.HasEndState())
            {
                var skillState = _skillStateQueue.Dequeue();
                _skillContext = new SkillContext(skillState);
            }

            _skillContext.Select();
            
            if (_skillContext.HasEndState() && _skillStateQueue.Count == 0)
            {
                Context.TransitionTo(_container.Resolve<TurnEndState>());
            }
        }

        private void SetSkillContextQueue()
        {
            var skill = _skillFactory.Create(_skillCode);
            var skillStates = Enumerable.Empty<AbstractSkillState>()
                .Concat(skill.AilmentParameterList.Select(x => new AilmentState(x)))
                .Concat(skill.DamageParameterList.Select(x => _damageStateFactory.Create(skill.SkillCommon, x)))
                .Concat(skill.BuffParameterList.Select(x => _buffStateFactory.Create(skill.SkillCommon, x)))
                .Concat(skill.RestoreParameterList.Select(x => _restoreStateFactory.Create(skill.SkillCommon, x)));
            _skillStateQueue = new Queue<AbstractSkillState>(skillStates);
        }
    }
}
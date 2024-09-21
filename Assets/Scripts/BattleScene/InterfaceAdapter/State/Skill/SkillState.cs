using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.InterfaceAdapter.State.Battle;
using BattleScene.UseCases.Service;
using UnityEngine;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SkillState : AbstractState
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillPropertyDto, SkillCode> _skillViewResource;
        private readonly SkillExecutorService _skillExecutor;
        private readonly AilmentStateFactory _ailmentStateFactory;
        private readonly BuffStateFactory _buffStateFactory;
        private readonly DamageStateFactory _damageStateFactory;
        private readonly RestoreStateFactory _restoreStateFactory;
        private readonly SlipStateFactory _slipStateFactory;
        private readonly SkillStateQueueCreatorService _skillStateQueueCreator;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly SkillCode _skillCode;
        private readonly ImmutableList<CharacterId> _targetIdList;
        private Queue<AbstractSkillState> _skillStateQueue;
        private SkillContext _skillContext;

        public SkillState(
            SkillCode skillCode,
            IList<CharacterId> targetIdList,
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillPropertyDto, SkillCode> skillViewResource,
            SkillExecutorService skillExecutor,
            AilmentStateFactory ailmentStateFactory,
            BuffStateFactory buffStateFactory,
            DamageStateFactory damageStateFactory,
            RestoreStateFactory restoreStateFactory,
            SlipStateFactory slipStateFactory,
            SkillStateQueueCreatorService skillStateQueueCreator,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _skillCode = skillCode;
            _targetIdList = targetIdList.ToImmutableList();
            _container = container;
            _skillFactory = skillFactory;
            _skillViewResource = skillViewResource;
            _skillExecutor = skillExecutor;
            _ailmentStateFactory = ailmentStateFactory;
            _buffStateFactory = buffStateFactory;
            _damageStateFactory = damageStateFactory;
            _restoreStateFactory = restoreStateFactory;
            _slipStateFactory = slipStateFactory;
            _skillStateQueueCreator = skillStateQueueCreator;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public override void Start()
        {
            _skillStateQueue = _skillStateQueueCreator.Create(_skillCode, _targetIdList);
            _skillExecutor.Execute(_skillCode);
            var skill = _skillFactory.Create(_skillCode);
            _messageView.StartMessageAnimationAsync(skill.SkillCommon.MessageCode);
            var playerImageCode = _skillViewResource.Get(skill.SkillCommon.SkillCode).PlayerImageCode;
            _playerImageView.StartAnimationAsync(playerImageCode);
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
                .Concat(CreateAilmentState(skill))
                .Concat(skill.DamageParameterList
                    .Select(x => _damageStateFactory.Create(
                        skillCommon: skill.SkillCommon,
                        damageParameter: x,
                        targetIdList: _targetIdList)))
                .Concat(CreateBuffState(skill))
                .Concat(skill.RestoreParameterList.Select(x => _restoreStateFactory.Create(skill.SkillCommon, x)))
                .Concat(skill.SlipParameterList.Select(x => _slipStateFactory.Create(
                    skillCommon: skill.SkillCommon,
                    slipParameter: x,
                    targetIdList: _targetIdList)));
            _skillStateQueue = new Queue<AbstractSkillState>(skillStates);
        }

        private IEnumerable<AbstractSkillState> CreateAilmentState(SkillValueObject skill)
        {
            var ailmentStates = Enumerable.Empty<AbstractSkillState>();
            if (skill.AilmentParameterList.IsEmpty) return ailmentStates;
            var ailmentState = _ailmentStateFactory.Create(
                skillCommon: skill.SkillCommon,
                ailmentParameterList: skill.AilmentParameterList,
                targetIdList: _targetIdList);
            ailmentStates = ailmentStates.Append(ailmentState);
            return ailmentStates;
        }

        private IEnumerable<AbstractSkillState> CreateBuffState(SkillValueObject skill)
        {
            var buffStates = Enumerable.Empty<AbstractSkillState>();
            if (skill.BuffParameterList.IsEmpty) return buffStates;
            var buffState = _buffStateFactory.Create(
                skillCommon: skill.SkillCommon,
                buffParameterList: skill.BuffParameterList,
                targetIdList: _targetIdList);
            buffStates = buffStates.Append(buffState);
            return buffStates;
        }
    }
}
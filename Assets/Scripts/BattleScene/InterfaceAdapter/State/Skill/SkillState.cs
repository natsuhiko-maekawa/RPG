using System.Collections.Generic;
using System.Collections.Immutable;
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
    }
}
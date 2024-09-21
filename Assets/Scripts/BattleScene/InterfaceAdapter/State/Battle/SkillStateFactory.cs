using System.Collections.Generic;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.InterfaceAdapter.State.Skill;
using BattleScene.UseCases.Service;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SkillStateFactory
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillPropertyDto, SkillCode> _skillViewResource;
        private readonly SkillExecutorService _skillExecutor;
        private readonly SkillStateQueueCreatorService _skillStateQueueCreator;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        
        public SkillStateFactory(
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillPropertyDto, SkillCode> skillViewResource,
            SkillExecutorService skillExecutor,
            SkillStateQueueCreatorService skillStateQueueCreator,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _container = container;
            _skillFactory = skillFactory;
            _skillViewResource = skillViewResource;
            _skillExecutor = skillExecutor;
            _skillStateQueueCreator = skillStateQueueCreator;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public SkillState Create(SkillCode skillCode, IList<CharacterId> targetIdList) => new(
            skillCode: skillCode,
            targetIdList: targetIdList,
            container: _container, 
            skillFactory: _skillFactory,
            skillViewResource: _skillViewResource,
            skillExecutor: _skillExecutor,
            skillStateQueueCreator: _skillStateQueueCreator,
            messageView: _messageView,
            playerImageView: _playerImageView);
    }
}
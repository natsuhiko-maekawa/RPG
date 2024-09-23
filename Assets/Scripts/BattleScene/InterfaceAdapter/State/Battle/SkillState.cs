using System.Collections.Generic;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.InterfaceAdapter.State.Skill;
using BattleScene.UseCases.Service;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SkillState : AbstractState
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillPropertyDto, SkillCode> _skillViewResource;
        private readonly SkillExecutorService _skillExecutor;
        private readonly PrimeSkillContextService _primeSkillContext;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private Queue<ISkillContext> _skillStateQueue;
        private ISkillContext _skillContext;

        public SkillState(
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillPropertyDto, SkillCode> skillViewResource,
            SkillExecutorService skillExecutor,
            PrimeSkillContextService primeSkillContext,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _container = container;
            _skillFactory = skillFactory;
            _skillViewResource = skillViewResource;
            _skillExecutor = skillExecutor;
            _primeSkillContext = primeSkillContext;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public override void Start()
        {
            _primeSkillContext.Start(Context);
            _skillExecutor.Execute(Context.SkillCode);
            var skill = _skillFactory.Create(Context.SkillCode);
            _messageView.StartMessageAnimationAsync(skill.SkillCommon.MessageCode);
            var playerImageCode = _skillViewResource.Get(skill.SkillCommon.SkillCode).PlayerImageCode;
            _playerImageView.StartAnimationAsync(playerImageCode);
        }

        public override void Select()
        {
            var value = _primeSkillContext.Select();
            
            if (!value)
            {
                Context.TransitionTo(_container.Resolve<TurnEndState>());
            }
        }
    }
}
using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.FrameView.OutputBoundary;
using BattleScene.UseCase.View.FrameView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputData;
using Utility;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Event
{
    internal class EnemySelectSkillEvent : IEvent, IWait
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IFrameViewPresenter _frameView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderItems;
        private readonly IPlayerImageViewPresenter _playerImageView;
        private readonly IRandomEx _randomEx;
        private readonly SkillCreatorService _skillCreatorService;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;
        private readonly TargetDomainService _target;
        private readonly TargetFrameOutputDataFactory _targetFrameOutputDataFactory;
        private readonly ITargetRepository _targetRepository;

        public EnemySelectSkillEvent(
            ICharacterRepository characterRepository,
            IFrameViewPresenter frameView,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderItems,
            IPlayerImageViewPresenter playerImageView,
            IRandomEx randomEx,
            SkillCreatorService skillCreatorService,
            ISkillRepository skillRepository,
            ISkillViewInfoFactory skillViewInfoFactory,
            TargetDomainService target,
            TargetFrameOutputDataFactory targetFrameOutputDataFactory,
            ITargetRepository targetRepository)
        {
            _characterRepository = characterRepository;
            _frameView = frameView;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _orderItems = orderItems;
            _playerImageView = playerImageView;
            _randomEx = randomEx;
            _skillCreatorService = skillCreatorService;
            _skillRepository = skillRepository;
            _skillViewInfoFactory = skillViewInfoFactory;
            _target = target;
            _targetFrameOutputDataFactory = targetFrameOutputDataFactory;
            _targetRepository = targetRepository;
        }

        public EventCode Run()
        {
            // TODO: 敵がスキルを選択する際、ランダムに選択する仮のアルゴリズムを実装している
            var characterId = _orderItems.FirstCharacterId();
            var skillCodeList = _characterRepository.Select(characterId).GetSkills();
            var skillCode = _randomEx.Choice(skillCodeList);
            var skill = _skillCreatorService.Create(characterId, skillCode);

            _skillRepository.Update(skill);

            var target = _targetRepository.Select(characterId);
            target.Set(_target.Get(characterId, skill.AbstractSkill.GetRange())
                .ToImmutableList());
            _targetRepository.Update(target);

            _frameView.Start(_targetFrameOutputDataFactory.Create());
            var messageOutputData = _messageOutputDataFactory.Create(SkillMessage);
            _messageView.Start(messageOutputData);
            _playerImageView.Start(new PlayerImageOutputData(_skillViewInfoFactory.Create(skillCode).PlayerImageCode));

            return EventCode.WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.EnemyAttackEvent;
        }
    }
}
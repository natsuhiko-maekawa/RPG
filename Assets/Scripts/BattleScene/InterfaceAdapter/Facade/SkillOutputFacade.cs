using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.State.Battle;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class SkillOutputFacade
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillPropertyDto, SkillCode> _skillViewResource;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public SkillOutputFacade(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillPropertyDto, SkillCode> skillViewResource,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            OrderedItemsDomainService orderedItems,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _skillFactory = skillFactory;
            _skillViewResource = skillViewResource;
            _battleLogRepository = battleLogRepository;
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public async Task Output(Context context)
        {
            var animationList = new List<Task>();

            var skill = _skillFactory.Create(context.SkillCode);
            var messageAnimation = _messageView.StartMessageAnimationAsync(skill.SkillCommon.MessageCode);
            animationList.Add(messageAnimation);

            _orderedItems.First().TryGetCharacterId(out var actorId);
            var isActorPlayer = _characterRepository.Select(actorId).IsPlayer;
            var playerSkillCode = isActorPlayer
                ? context.SkillCode
                : _battleLogRepository.Select()
                    .Last(x => _characterRepository.Select(x.ActorId).IsPlayer)
                    .SkillCode;
            var playerImageCode = _skillViewResource.Get(playerSkillCode).PlayerImageCode;
            var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode);
            animationList.Add(playerImageAnimation);

            await Task.WhenAll(animationList);
        }
    }
}
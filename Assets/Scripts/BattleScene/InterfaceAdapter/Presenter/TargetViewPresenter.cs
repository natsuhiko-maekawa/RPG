using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class TargetViewPresenter
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerDomainService _player;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly TargetDomainService _target;
        private readonly TargetView _targetView;

        public TargetViewPresenter(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            OrderedItemsDomainService orderedItems,
            TargetDomainService target,
            PlayerDomainService player,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            TargetView targetView)
        {
            _skillFactory = skillFactory;
            _orderedItems = orderedItems;
            _target = target;
            _player = player;
            _characterRepository = characterRepository;
            _targetView = targetView;
        }

        public void StartAnimation(SkillCode skillCode)
        {
            _orderedItems.First().TryGetCharacterId(out var characterId);
            var range = _skillFactory.Create(skillCode).SkillCommon.Range;
            var targetIdList = _target.Get(
                characterId,
                range);
            var characterDtoList = targetIdList
                .Select(CreateCharacterDto)
                .ToImmutableList();
            var targetViewDto = new TargetViewDto(characterDtoList);
            _targetView.StartAnimation(targetViewDto);
        }

        public void StopAnimation()
        {
            _targetView.StopAnimation();
        }

        private CharacterDto CreateCharacterDto(CharacterId x)
        {
            var characterDto = Equals(x, _player.GetId())
                ? CharacterDto.CreatePlayer()
                : new CharacterDto(_characterRepository.Select(x).Position);
            return characterDto;
        }
    }
}
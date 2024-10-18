using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class TargetViewPresenter
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerDomainService _player;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly TargetService _target;
        private readonly TargetView _targetView;

        public TargetViewPresenter(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            OrderedItemsDomainService orderedItems,
            TargetService target,
            PlayerDomainService player,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            TargetView targetView)
        {
            _skillFactory = skillFactory;
            _orderedItems = orderedItems;
            _target = target;
            _player = player;
            _characterCollection = characterCollection;
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
                .ToList();
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
                : new CharacterDto(_characterCollection.Get(x).Position);
            return characterDto;
        }
    }
}
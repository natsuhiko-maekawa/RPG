using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.IService;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class TargetViewPresenter
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly ITargetService _target;
        private readonly TargetView _targetView;

        public TargetViewPresenter(
            ITargetService target,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            TargetView targetView)
        {
            _target = target;
            _characterCollection = characterCollection;
            _targetView = targetView;
        }

        public void StartAnimation(CharacterId actorId, SkillValueObject skill)
        {
            var range = skill.Common.Range;
            var targetIdList = _target.Get(actorId, range);

            var characterList = _characterCollection.Get(targetIdList);
            var characterStructList = characterList
                // C#11以前で静的メソッドグループをデリゲート化するとアロケーションが発生するため、
                // メソッドグループは使用しない。
                .Select(x => CreateCharacterStruct(x))
                .ToArray();
            var targetViewDto = new TargetViewDto(characterStructList);
            _targetView.StartAnimation(targetViewDto);
        }

        public void StopAnimation()
        {
            _targetView.StopAnimation();
        }

        private static CharacterStruct CreateCharacterStruct(CharacterEntity character)
        {
            var characterStruct = character.IsPlayer
                ? CharacterStruct.CreatePlayer()
                : CharacterStruct.CreateEnemy(character.Position);
            return characterStruct;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using UnityEngine;
using VContainer;

namespace BattleScene.UseCases.Service.DebugService
{
    public class DebugEnemiesRegistererService : MonoBehaviour, IEnemiesRegistererService
    {
        [SerializeField] private CharacterTypeCode[] characterTypeCodeArray;
        private IFactory<CharacterPropertyValueObject, CharacterTypeCode> _propertyFactory;
        private ICollection<CharacterEntity, CharacterId> _characterCollection;

        [Inject]
        public void Construct(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> propertyFactory,
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _propertyFactory = propertyFactory;
            _characterCollection = characterCollection;
        }
        
        public void Register(IList<CharacterTypeCode> characterTypeIdList)
        {
            var characterList = characterTypeCodeArray
                .Select(GetCharacter)
                .ToList();
            _characterCollection.Add(characterList);
        }

        private CharacterEntity GetCharacter(CharacterTypeCode characterTypeCode, int position)
        {
            CharacterPropertyValueObject characterProperty = _propertyFactory.Create(characterTypeCode);
            var characterId = new CharacterId();
            return new CharacterEntity(
                id: characterId,
                characterTypeCode: characterProperty.CharacterTypeCode,
                currentHitPoint: characterProperty.HitPoint,
                position: position);
        }
    }
}
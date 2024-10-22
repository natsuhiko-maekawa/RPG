using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class DamageRegistererService : IPrimeSkillRegistererService<PrimeSkillValueObject>
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;

        public DamageRegistererService(
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _characterCollection = characterCollection;
        }

        public void Register(PrimeSkillValueObject damage)
        {
            var characterList = damage.DamageDictionary
                .Select(ReduceHitPoint)
                .ToList();
            _characterCollection.Add(characterList);
        }

        public void Register(IReadOnlyList<PrimeSkillValueObject> damageList)
        {
            foreach (var damage in damageList) Register(damage);
        }

        private CharacterEntity ReduceHitPoint(KeyValuePair<CharacterId, int> characterIdDamagePair)
        {
            var characterId = characterIdDamagePair.Key;
            var character = _characterCollection.Get(characterId);
            var currentHitPoint = character.CurrentHitPoint;
            var damage = characterIdDamagePair.Value;
            var reducedHitPoint = currentHitPoint - damage;
            character.CurrentHitPoint = reducedHitPoint;
            return character;
        }
    }
}
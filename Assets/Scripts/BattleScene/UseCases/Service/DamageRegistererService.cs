using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class DamageRegistererService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public DamageRegistererService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public void Register(DamageValueObject damage)
        {
            var characterList = damage.GetDamageDictionary()
                .Select(ReduceHitPoint)
                .ToImmutableList();
            _characterRepository.Update(characterList);
        }

        public void Register(IReadOnlyList<DamageValueObject> damageList)
        {
            foreach (var damage in damageList) Register(damage);
        }

        private CharacterEntity ReduceHitPoint(KeyValuePair<CharacterId, int> characterIdDamagePair)
        {
            var characterId = characterIdDamagePair.Key;
            var character = _characterRepository.Select(characterId);
            var currentHitPoint = character.CurrentHitPoint;
            var damage = characterIdDamagePair.Value;
            var reducedHitPoint = currentHitPoint - damage;
            character.CurrentHitPoint = reducedHitPoint;
            return character;
        }
    }
}
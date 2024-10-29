using System.Collections.Generic;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class HitPointService : IHitPointService
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;

        public HitPointService(
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _characterCollection = characterCollection;
        }

        public void Damaged(BattleEventValueObject damageEvent)
        {
            foreach (var (damagedCharacterId, damageAmount) in damageEvent.DamageDictionary)
            {
                var character = _characterCollection.Get(damagedCharacterId);
                character.CurrentHitPoint -= damageAmount;
            }
        }

        public void Damaged(IReadOnlyList<BattleEventValueObject> damageEventList)
        {
            foreach (var damageEvent in damageEventList) Damaged(damageEvent);
        }
    }
}
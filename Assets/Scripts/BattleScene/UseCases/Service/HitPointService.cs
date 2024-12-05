using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class HitPointService : IHitPointService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public HitPointService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public void Damaged(BattleEventValueObject damageEvent)
        {
            foreach (var (damagedCharacterId, damageAmount) in damageEvent.DamageDictionary)
            {
                var character = _characterRepository.Get(damagedCharacterId);
                character.CurrentHitPoint -= damageAmount;
            }
        }

        public void Damaged(IReadOnlyList<BattleEventValueObject> damageEventList)
        {
            foreach (var damageEvent in damageEventList) Damaged(damageEvent);
        }

        public void Cure(IReadOnlyList<BattleEventValueObject> cureEventList)
        {
            foreach (var curing in cureEventList.SelectMany(x => x.CuringList))
            {
                var character = _characterRepository.Get(curing.TargetId);
                character.CurrentHitPoint += curing.Amount;
            }
        }
    }
}
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class SkillExecutorService
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly OrderedItemsDomainService _orderedItems;

        public SkillExecutorService(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            OrderedItemsDomainService orderedItems)
        {
            _characterCollection = characterCollection;
            _orderedItems = orderedItems;
        }

        public void Execute(SkillValueObject skill)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) return;
            if (!_characterCollection.Get(actorId).IsPlayer) return;

            var technicalPoint = skill.Common.TechnicalPoint;
            _characterCollection.Get(actorId).CurrentTechnicalPoint -= technicalPoint;
        }
    }
}
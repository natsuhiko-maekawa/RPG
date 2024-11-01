using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.Service
{
    public class BuffTurnService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly ICollection<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceCollection;

        public BuffTurnService(
            OrderedItemsDomainService orderedItems,
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            ICollection<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceCollection)
        {
            _orderedItems = orderedItems;
            _buffCollection = buffCollection;
            _characterCollection = characterCollection;
            _enhanceCollection = enhanceCollection;
        }

        public void Advance()
        {
            foreach (var buff in _buffCollection.Get()
                         .Where(x => x.LifetimeCode == LifetimeCode.ToEndTurn || IsNextAction(x.LifetimeCode)))
            {
                buff.AdvanceTurn();
            }

            foreach (var enhance in _enhanceCollection.Get()
                         .Where(x => x.LifetimeCode == LifetimeCode.ToEndTurn || IsNextAction(x.LifetimeCode)))
            {
                enhance.AdvanceTurn();
            }

            var removeIdList = _enhanceCollection.Get()
                .Where(x => !x.Effects)
                .Select(x => x.Id)
                .ToList();
            _enhanceCollection.Remove(removeIdList);
        }

        private bool IsNextAction(LifetimeCode lifetimeCode)
        {
            if (lifetimeCode != LifetimeCode.ToNextAction) return false;
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) return false;
            if (!_characterCollection.Get(characterId).IsPlayer) return false;
            return true;
        }
    }
}
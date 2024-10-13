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
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public BuffTurnService(
            OrderedItemsDomainService orderedItems,
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _orderedItems = orderedItems;
            _buffRepository = buffRepository;
            _characterRepository = characterRepository;
        }

        public void Advance()
        {
            foreach (var buff in _buffRepository.Select()
                         .Where(x => x.LifetimeCode == LifetimeCode.ToEndTurn || IsNextAction(x.LifetimeCode)))
            { 
                buff.AdvanceTurn();
            }
        }

        private bool IsNextAction(LifetimeCode lifetimeCode)
        {
            if (lifetimeCode != LifetimeCode.ToNextAction) return false;
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) return false;
            if (!_characterRepository.Select(characterId).IsPlayer) return false;
            return true;
        }
    }
}
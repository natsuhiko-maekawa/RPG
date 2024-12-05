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
        private readonly IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceRepository;

        public BuffTurnService(
            OrderedItemsDomainService orderedItems,
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceRepository)
        {
            _orderedItems = orderedItems;
            _buffRepository = buffRepository;
            _characterRepository = characterRepository;
            _enhanceRepository = enhanceRepository;
        }

        public void Advance()
        {
            foreach (var buff in _buffRepository.Get()
                         .Where(x => x.LifetimeCode == LifetimeCode.ToEndTurn || IsNextAction(x.LifetimeCode)))
            {
                buff.AdvanceTurn();
            }

            foreach (var enhance in _enhanceRepository.Get()
                         .Where(x => x.LifetimeCode == LifetimeCode.ToEndTurn || IsNextAction(x.LifetimeCode)))
            {
                enhance.AdvanceTurn();
            }
        }

        private bool IsNextAction(LifetimeCode lifetimeCode)
        {
            if (lifetimeCode != LifetimeCode.ToNextAction) return false;
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) return false;
            if (!_characterRepository.Get(characterId).IsPlayer) return false;
            return true;
        }
    }
}
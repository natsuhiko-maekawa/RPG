using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using Utility.Interface;

namespace BattleScene.InterfaceAdapter.Service
{
    public class ActorService
    {
        private readonly AilmentDomainService _ailment;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRandomEx _randomEx;

        public ActorService(
            AilmentDomainService ailment,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            OrderedItemsDomainService orderedItems,
            IRandomEx randomEx)
        {
            _ailment = ailment;
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
            _randomEx = randomEx;
        }

        public bool IsResetAilment => _orderedItems.First().OrderedItemType == OrderedItemType.Ailment;
        public bool IsSlipDamage => _orderedItems.First().OrderedItemType == OrderedItemType.Slip;
        public bool CantAction => CantCharacterAction();
        public bool IsPlayer
        {
            get
            {
                var isCharacter = _orderedItems.First().TryGetCharacterId(out var characterId);
                var isPlayer = isCharacter && _characterRepository.Select(characterId).IsPlayer;
                return isPlayer;
            }
        }
        
        private bool CantCharacterAction()
        {
            var actorIsCharacter = _orderedItems.First().TryGetCharacterId(out var characterId);
            if (!actorIsCharacter) return false;
            
            var ailmentCode = _ailment.GetHighestPriority(characterId)?.AilmentCode;
            if (!ailmentCode.HasValue) return false;

            var absoluteCantAction = ailmentCode.Value is not (AilmentCode.Paralysis or AilmentCode.EnemyParalysis);
            return absoluteCantAction || CantActionBecauseParalysis;
        }

        private bool CantActionBecauseParalysis => _randomEx.Probability(0.5f);
    }
}
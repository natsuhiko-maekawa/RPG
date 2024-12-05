using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.InterfaceAdapter.Service.Replacement
{
    public class ReplacementCommonService
    {
        internal const string TotalPrefix = "計";
        internal const string TotalSuffix = "たち";
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly IResource<PlayerViewDto, CharacterTypeCode> _playerViewInfoResource;

        public ReplacementCommonService(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            IResource<PlayerViewDto, CharacterTypeCode> playerViewInfoResource)
        {
            _characterRepository = characterRepository;
            _enemyViewInfoResource = enemyViewInfoResource;
            _playerViewInfoResource = playerViewInfoResource;
        }

        public string GetCharacterName(CharacterId characterId)
        {
            var character = _characterRepository.Get(characterId);
            var characterName = character.IsPlayer
                ? _playerViewInfoResource.Get(CharacterTypeCode.Player).Name
                : _enemyViewInfoResource.Get(character.CharacterTypeCode).Name;
            return characterName;
        }
    }
}
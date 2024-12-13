using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;

namespace BattleScene.InterfaceAdapter.Services.Replacements
{
    public class ReplacementCommonService
    {
        internal const string TotalPrefix = "計";
        internal const string TotalSuffix = "たち";
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly IResource<PlayerViewDto, CharacterTypeCode> _playerViewInfoResource;

        public ReplacementCommonService(
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            IResource<PlayerViewDto, CharacterTypeCode> playerViewInfoResource)
        {
            _enemyViewInfoResource = enemyViewInfoResource;
            _playerViewInfoResource = playerViewInfoResource;
        }

        public string GetCharacterName(CharacterEntity character)
        {
            var characterName = character.IsPlayer
                ? _playerViewInfoResource.Get(CharacterTypeCode.Player).Name
                : _enemyViewInfoResource.Get(character.CharacterTypeCode).Name;
            return characterName;
        }
    }
}
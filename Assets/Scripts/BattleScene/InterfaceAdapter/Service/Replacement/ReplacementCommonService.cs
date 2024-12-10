using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;

namespace BattleScene.InterfaceAdapter.Service.Replacement
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
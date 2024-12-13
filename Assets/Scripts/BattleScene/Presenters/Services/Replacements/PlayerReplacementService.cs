using System;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.Services.Replacements.Interfaces;

namespace BattleScene.Presenters.Services.Replacements
{
    public class PlayerReplacementService : IReplacementService
    {
        public string Replacement => "[player]";
        private readonly IResource<PlayerViewDto, CharacterTypeCode> _playerViewInfoResource;

        public PlayerReplacementService(
            IResource<PlayerViewDto, CharacterTypeCode> playerViewInfoResource)
        {
            _playerViewInfoResource = playerViewInfoResource;
        }

        public bool IsMatch(string value) => value == Replacement;
        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var playerName = _playerViewInfoResource.Get(CharacterTypeCode.Player).Name;
            return playerName;
        }
    }
}
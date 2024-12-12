using System;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Services.Replacements.Interfaces;

namespace BattleScene.InterfaceAdapter.Services.Replacements
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
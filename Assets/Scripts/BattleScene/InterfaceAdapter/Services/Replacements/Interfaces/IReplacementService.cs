using System;

namespace BattleScene.InterfaceAdapter.Services.Replacements.Interfaces
{
    public interface IReplacementService
    {
        public string Replacement { get; }
        public bool IsMatch(string value);
        public ReadOnlySpan<char> GetNewCharSpan();
    }
}
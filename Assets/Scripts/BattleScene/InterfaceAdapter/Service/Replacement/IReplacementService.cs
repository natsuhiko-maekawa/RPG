using System;

namespace BattleScene.InterfaceAdapter.Service.Replacement
{
    public interface IReplacementService
    {
        public string Replacement { get; }
        public bool IsMatch(string value);
        public ReadOnlySpan<char> GetNewCharSpan();
    }
}
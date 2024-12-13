using System;

namespace BattleScene.Presenters.Services.Replacements.Interfaces
{
    // TODO: 抽象クラスの方が良かったかもしれない。
    public interface IReplacementService
    {
        public string Replacement { get; }
        public bool IsMatch(string value);
        public ReadOnlySpan<char> GetNewCharSpan();
    }
}
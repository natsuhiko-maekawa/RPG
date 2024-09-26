using System.Collections.Generic;

namespace BattleScene.Framework.ViewModel
{
    public record DigitViewModel(
        IReadOnlyList<Digit> DigitList);

    public record Digit(
        DigitType DigitType,
        int Number,
        bool IsAvoid = false,
        int Index = 0);

    public enum DigitType
    {
        Damage,
        Cure,
        Restore
    }
}
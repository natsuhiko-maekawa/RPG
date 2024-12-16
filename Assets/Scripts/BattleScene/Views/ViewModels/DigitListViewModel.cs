using System.Collections.Generic;

namespace BattleScene.Views.ViewModels
{
    public record DigitListViewModel(
        IReadOnlyList<DigitViewModel> DigitList);

    public record DigitViewModel(
        DigitType DigitType,
        int Digit,
        bool IsAvoid = false,
        int Index = 0);

    public enum DigitType
    {
        Damage,
        Cure,
        Restore
    }
}
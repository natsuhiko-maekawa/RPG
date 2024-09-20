using System.Collections.Generic;

namespace BattleScene.Framework.ViewModel
{
    public record DigitViewModel(
        IReadOnlyList<DigitValueObject> DigitList);
    
    public record PlayerDigitViewDto(
        IList<DigitValueObject> DigitDtoList);

    public record EnemyDigitViewDto(
        int EnemyInt,
        IList<DigitValueObject> DigitDtoList);

    public record DigitValueObject(
        int Index,
        int Digit,
        bool IsAvoid,
        DigitColor DigitColor);

    public enum DigitColor
    {
        Orange,
        Green,
        Blue
    }
}
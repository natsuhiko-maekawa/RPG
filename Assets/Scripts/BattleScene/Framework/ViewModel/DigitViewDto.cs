using System.Collections.Generic;

namespace BattleScene.Framework.ViewModel
{
    public record PlayerDigitViewDto(
        IList<DigitDto> DigitDtoList);

    public record EnemyDigitViewDto(
        int EnemyInt,
        IList<DigitDto> DigitDtoList);

    public record DigitDto(
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
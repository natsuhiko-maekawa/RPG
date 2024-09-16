using System;
using System.Collections.Immutable;

namespace BattleScene.Framework.ViewModel
{
    public record PlayerAilmentsViewDto(
        ImmutableList<int> AilmentNumberList);

    public record EnemyAilmentsViewDto(
        int EnemyNumber,
        ImmutableList<int> AilmentNumberList);

    [Obsolete]
    public record AilmentsDto(
        int AilmentsInt);
}
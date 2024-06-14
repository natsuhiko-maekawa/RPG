using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BattleScene.InterfaceAdapter.Presenter.AilmentsView
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
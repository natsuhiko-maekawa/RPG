using System;
using System.Collections.Immutable;

namespace BattleScene.UseCases.OutputData
{
    [Obsolete]
    public record GridViewOutputData(
        ImmutableList<Row> Row);
}
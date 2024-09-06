using System.Collections.Immutable;

namespace BattleScene.UseCases.OutputData
{
    public record GridViewOutputData(
        ImmutableList<Row> Row);
}
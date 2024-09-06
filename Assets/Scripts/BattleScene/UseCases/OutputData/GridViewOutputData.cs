using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.UseCases.OutputData
{
    public record GridViewOutputData(
        ImmutableList<Row> Row);

    public record Row(
        ActionCode ActionCode,
        bool Enabled);
}
using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.UseCases.View.GridView
{
    public record GridViewOutputData(
        ImmutableList<Row> Row) : IOutputData;

    public record Row(
        ActionCode ActionCode,
        bool Enabled);
}
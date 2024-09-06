using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.View;

namespace BattleScene.UseCases.OutputData
{
    public record GridViewOutputData(
        ImmutableList<Row> Row) : IOutputData;

    public record Row(
        ActionCode ActionCode,
        bool Enabled);
}
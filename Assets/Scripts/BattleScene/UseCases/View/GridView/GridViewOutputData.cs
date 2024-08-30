using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.UseCases.View.GridView
{
    public record GridViewOutputData(
        ImmutableList<ActionCode> ActionCodeList) : IOutputData;
}
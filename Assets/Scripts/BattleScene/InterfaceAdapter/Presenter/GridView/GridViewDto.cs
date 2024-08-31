using System.Collections.Immutable;
using BattleScene.InterfaceAdapter.Presenter.Dto.Interface;

namespace BattleScene.InterfaceAdapter.Presenter.GridView
{
    public record GridViewDto(
        ImmutableList<RowDto> RowList) : IDto;

    public record RowDto(
        int RowId,
        string RowName,
        string RowDescription,
        string PlayerImagePath,
        bool Enabled);
}
using System.Collections.Immutable;

namespace BattleScene.InterfaceAdapter.Presenter.GridView
{
    public record GridViewDto(
        ImmutableList<RowDto> RowList);

    public record RowDto(
        int RowId,
        string RowName,
        string RowDescription,
        string PlayerImagePath,
        bool Enabled);
}
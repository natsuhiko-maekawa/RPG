using System.Collections.Immutable;

namespace BattleScene.InterfaceAdapter.Presenter.Dto
{
    public record GridViewDto(
        ImmutableList<RowDto> RowList);
}
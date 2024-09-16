using System.Collections.Immutable;
using BattleScene.InterfaceAdapter.Code;

namespace BattleScene.InterfaceAdapter.Presenter.Dto
{
    public record GridViewDto(
        ActionCode ActionCode,
        ImmutableList<RowDto> RowDtoList);
}
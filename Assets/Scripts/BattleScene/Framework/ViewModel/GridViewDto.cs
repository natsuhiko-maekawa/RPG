using System.Collections.Immutable;
using BattleScene.Framework.Code;

namespace BattleScene.Framework.ViewModel
{
    public record GridViewDto(
        ActionCode ActionCode,
        ImmutableList<RowDto> RowDtoList);
}
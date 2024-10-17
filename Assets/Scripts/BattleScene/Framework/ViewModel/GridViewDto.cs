using System.Collections.Generic;
using BattleScene.Framework.Code;

namespace BattleScene.Framework.ViewModel
{
    public record GridViewDto(
        ActionCode ActionCode,
        IReadOnlyList<RowDto> RowDtoList);
}
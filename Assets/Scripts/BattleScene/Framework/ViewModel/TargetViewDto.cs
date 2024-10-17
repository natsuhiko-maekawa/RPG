using System.Collections.Generic;

namespace BattleScene.Framework.ViewModel
{
    public record TargetViewDto(
        IReadOnlyList<CharacterDto> CharacterDtoList);
}
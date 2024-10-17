using System.Collections.Generic;
using System.Collections.Immutable;

namespace BattleScene.Framework.ViewModel
{
    public record TargetViewDto(
IReadOnlyList<CharacterDto> CharacterDtoList);
}
using System.Collections.Immutable;

namespace BattleScene.Framework.ViewModel
{
    public record TargetViewDto(
        ImmutableList<CharacterDto> CharacterDtoList);
}
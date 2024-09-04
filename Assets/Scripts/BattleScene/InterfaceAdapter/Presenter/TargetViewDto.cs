using System.Collections.Immutable;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public record TargetViewDto(
        ImmutableList<CharacterDto> CharacterDtoList);
}
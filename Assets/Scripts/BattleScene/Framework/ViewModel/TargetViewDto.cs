using System.Collections.Immutable;

namespace BattleScene.InterfaceAdapter.Presenter.Dto
{
    public record TargetViewDto(
        ImmutableList<CharacterDto> CharacterDtoList);
}
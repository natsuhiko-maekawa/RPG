using System.Collections.Immutable;

namespace BattleScene.InterfaceAdapter.Presenter.Dto
{
    public record SkillViewDto(
        ImmutableList<SkillRowDto> SkillRowDtoList);
}
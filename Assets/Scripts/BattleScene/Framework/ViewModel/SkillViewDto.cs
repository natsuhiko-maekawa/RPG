using System.Collections.Immutable;

namespace BattleScene.Framework.ViewModel
{
    public record SkillViewDto(
        ImmutableList<SkillRowDto> SkillRowDtoList);
}
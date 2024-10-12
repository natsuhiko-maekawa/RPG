using System.Collections.Immutable;

namespace BattleScene.Framework.ViewModel
{
    public record SkillViewModel(
        ImmutableList<SkillRowDto> SkillRowDtoList);
}
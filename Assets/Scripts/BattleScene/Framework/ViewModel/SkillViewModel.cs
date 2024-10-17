using System.Collections.Generic;

namespace BattleScene.Framework.ViewModel
{
    public record SkillViewModel(
        IReadOnlyList<SkillRowDto> SkillRowDtoList);
}
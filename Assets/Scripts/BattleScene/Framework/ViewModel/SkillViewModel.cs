using System.Collections.Generic;
using System.Collections.Immutable;

namespace BattleScene.Framework.ViewModel
{
    public record SkillViewModel(
IReadOnlyList<SkillRowDto> SkillRowDtoList);
}
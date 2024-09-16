using System.Collections.Generic;

namespace BattleScene.InterfaceAdapter.Presenter.SelectSkillView
{
    public record SelectSkillViewDto(
        IList<SkillDto> SkillDtoList,
        int HighlightRow,
        bool ViewUpArrow,
        bool ViewDownArrow);

    public record SkillDto(
        string Name,
        int Tp,
        bool Disabled);
}
using System.Collections.Generic;

namespace BattleScene.Views.ViewModels
{
    public record SelectSkillViewModel(
        IReadOnlyList<SkillModel> SkillList,
        int HighlightRow,
        bool ViewUpArrow,
        bool ViewDownArrow);

    public record SkillModel(
        string Name,
        int Tp,
        bool Disabled);
}
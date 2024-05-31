using System.Collections.Generic;

namespace BattleScene.InterfaceAdapter.Presenter.SelectActionView
{
    public record SelectActionViewDto(
        int ViewLength,
        IList<string> TextList,
        IList<int> DisabledRowList,
        int HighlightRow);
}
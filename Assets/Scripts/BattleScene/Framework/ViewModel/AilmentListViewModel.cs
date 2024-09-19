using System.Collections.Generic;

namespace BattleScene.Framework.ViewModel
{
    public record AilmentListViewModel(
        IReadOnlyList<AilmentViewModel> AilmentList);
}
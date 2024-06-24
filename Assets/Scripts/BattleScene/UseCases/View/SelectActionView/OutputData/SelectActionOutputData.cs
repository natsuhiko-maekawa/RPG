using System.Collections.Generic;

namespace BattleScene.UseCases.View.SelectActionView.OutputData
{
    public record SelectActionOutputData(
        int ActualViewLength,
        int Selection,
        IList<int> DisabledRowList);
}
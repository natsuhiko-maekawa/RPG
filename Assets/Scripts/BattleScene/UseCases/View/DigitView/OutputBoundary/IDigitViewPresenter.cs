using System.Collections.Generic;
using BattleScene.UseCases.View.DigitView.OutputData;

namespace BattleScene.UseCases.View.DigitView.OutputBoundary
{
    public interface IDigitViewPresenter
    {
        public void Start(IList<DigitOutputData> digitOutputDataList);
    }
}
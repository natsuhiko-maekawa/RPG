using System.Collections.Generic;
using BattleScene.UseCase.View.DigitView.OutputData;

namespace BattleScene.UseCase.View.DigitView.OutputBoundary
{
    public interface IDigitViewPresenter
    {
        public void Start(IList<DigitOutputData> digitOutputDataList);
    }
}
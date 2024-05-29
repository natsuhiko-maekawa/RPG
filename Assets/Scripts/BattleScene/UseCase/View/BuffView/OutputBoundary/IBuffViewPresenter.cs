using System.Collections.Generic;
using BattleScene.UseCase.View.BuffView.OutputData;

namespace BattleScene.UseCase.View.BuffView.OutputBoundary
{
    public interface IBuffViewPresenter
    {
        public void Start(BuffOutputData outputData);
        public void Start(IList<BuffOutputData> outputDataList);
    }
}
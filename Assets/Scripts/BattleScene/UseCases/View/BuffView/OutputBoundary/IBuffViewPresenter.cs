using System.Collections.Generic;
using BattleScene.UseCases.View.BuffView.OutputData;

namespace BattleScene.UseCases.View.BuffView.OutputBoundary
{
    public interface IBuffViewPresenter
    {
        public void Start(BuffOutputData outputData);
        public void Start(IList<BuffOutputData> outputDataList);
    }
}
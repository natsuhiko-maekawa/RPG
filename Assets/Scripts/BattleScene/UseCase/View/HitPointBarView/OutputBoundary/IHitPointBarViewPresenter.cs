using System.Collections.Generic;
using BattleScene.UseCase.View.HitPointBarView.OutputData;

namespace BattleScene.UseCase.View.HitPointBarView.OutputBoundary
{
    public interface IHitPointBarViewPresenter
    {
        public void Start(HitPointBarOutputData outputData);
        public void Start(IList<HitPointBarOutputData> outputDataList);
    }
}
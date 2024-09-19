using System;
using System.Collections.Generic;
using BattleScene.UseCases.View.HitPointBarView.OutputData;

namespace BattleScene.UseCases.View.HitPointBarView.OutputBoundary
{
    [Obsolete]
    public interface IHitPointBarViewPresenter
    {
        public void Start(HitPointBarOutputData outputData);
        public void Start(IList<HitPointBarOutputData> outputDataList);
    }
}
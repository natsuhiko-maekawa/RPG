using System;
using System.Collections.Generic;
using BattleScene.UseCases.View.AilmentView.OutputData;

namespace BattleScene.UseCases.View.AilmentView.OutputBoundary
{
    [Obsolete]
    public interface IAilmentViewPresenter
    {
        public void Start(AilmentOutputData ailmentOutputData);
        public void Start(IList<AilmentOutputData> ailmentOutputDataList);
    }
}
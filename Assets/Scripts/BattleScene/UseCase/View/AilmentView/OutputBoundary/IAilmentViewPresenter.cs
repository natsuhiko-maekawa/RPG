using System.Collections.Generic;
using BattleScene.UseCase.View.AilmentView.OutputData;

namespace BattleScene.UseCase.View.AilmentView.OutputBoundary
{
    public interface IAilmentViewPresenter
    {
        public void Start(AilmentOutputData ailmentOutputData);
        public void Start(IList<AilmentOutputData> ailmentOutputDataList);
    }
}
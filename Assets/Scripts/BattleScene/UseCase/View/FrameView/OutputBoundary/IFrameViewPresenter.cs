using System.Collections.Generic;
using BattleScene.UseCase.View.FrameView.OutputData;

namespace BattleScene.UseCase.View.FrameView.OutputBoundary
{
    public interface IFrameViewPresenter
    {
        public void Start(IList<FrameOutputData> outputDataList);
        public void Stop();
    }
}
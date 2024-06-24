using System.Collections.Generic;
using BattleScene.UseCases.View.FrameView.OutputData;

namespace BattleScene.UseCases.View.FrameView.OutputBoundary
{
    public interface IFrameViewPresenter
    {
        public void Start(IList<FrameOutputData> outputDataList);
        public void Stop();
    }
}
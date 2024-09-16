using System;

namespace BattleScene.UseCases.Interface
{
    [Obsolete]
    public interface IViewPresenter<TOutputData>
    {
        public void Start(TOutputData outputData);
        public void Stop();
    }
}
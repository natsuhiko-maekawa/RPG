namespace BattleScene.UseCases.Interface
{
    public interface IViewPresenter<TOutputData>
    {
        public void Start(TOutputData outputData);
        public void Stop();
    }
}
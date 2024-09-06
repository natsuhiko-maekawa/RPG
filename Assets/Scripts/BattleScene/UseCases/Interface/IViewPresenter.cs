namespace BattleScene.UseCases.Interface
{
    public interface IViewPresenter<TOutputData> where TOutputData : IOutputData 
    {
        public void Start(TOutputData outputData);
        public void Stop();
    }
}
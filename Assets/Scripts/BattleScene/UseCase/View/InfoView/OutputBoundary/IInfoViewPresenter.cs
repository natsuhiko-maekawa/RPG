namespace BattleScene.UseCase.View.InfoView.OutputBoundary
{
    public interface IInfoViewPresenter
    {
        public void StartInfoView(string info);
        public void Stop();
    }
}
namespace BattleScene.Views.ViewModels
{
    public struct StatusBarViewModel
    {
        public int MaxPoint { get; }
        public int CurrentPoint { get; }

        public StatusBarViewModel(int maxPoint,
            int currentPoint)
        {
            MaxPoint = maxPoint;
            CurrentPoint = currentPoint;
        }
    }
}
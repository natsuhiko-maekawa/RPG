namespace BattleScene.Views.ViewModels
{
    public struct PlayerViewModel
    {
        public PlayerViewModel(
            string playerImagePath)
        {
            PlayerImagePath = playerImagePath;
        }

        public string PlayerImagePath { get; }
    }
}
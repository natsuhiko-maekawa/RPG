namespace BattleScene.Framework.ViewModel
{
    public struct PlayerViewModel
    {
        public PlayerViewModel(
            string playerImage)
        {
            PlayerImage = playerImage;
        }

        public string PlayerImage { get; }
    }
}
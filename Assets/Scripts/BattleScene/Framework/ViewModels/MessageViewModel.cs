namespace BattleScene.Framework.ViewModels
{
    public struct MessageViewModel
    {
        public MessageViewModel(
            string[] message,
            bool noWait)
        {
            Message = message;
            NoWait = noWait;
        }

        public string[] Message { get; }
        public bool NoWait { get; }
    }
}
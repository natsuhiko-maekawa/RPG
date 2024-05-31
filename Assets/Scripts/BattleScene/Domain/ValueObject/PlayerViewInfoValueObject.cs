namespace BattleScene.Domain.ValueObject
{
    public class PlayerViewInfoValueObject
    {
        public PlayerViewInfoValueObject(string playerName)
        {
            PlayerName = playerName;
        }

        public string PlayerName { get; }
    }
}
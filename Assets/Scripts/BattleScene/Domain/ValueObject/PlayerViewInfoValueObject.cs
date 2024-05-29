namespace BattleScene.Domain.ValueObject
{
    public class PlayerViewInfoValueObject
    {
        public string PlayerName { get; }

        public PlayerViewInfoValueObject(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
namespace BattleScene.Framework.ViewModel
{
    public struct Character
    {
        public bool IsPlayer { get; }
        public int Position { get; }

        private Character(
            bool isPlayer,
            int position)
        {
            IsPlayer = isPlayer;
            Position = position;
        }

        public static Character CreatePlayer()
        {
            return new Character(
                isPlayer: true,
                position: 0);
        }

        public static Character CreateEnemy(int index)
        {
            return new Character(
                isPlayer: false,
                position: index);
        }
    }
}
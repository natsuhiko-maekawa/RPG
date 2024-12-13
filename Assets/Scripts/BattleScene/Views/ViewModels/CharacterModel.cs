namespace BattleScene.Views.ViewModels
{
    public struct CharacterModel
    {
        public bool IsPlayer { get; }
        public int Position { get; }

        private CharacterModel(
            bool isPlayer,
            int position)
        {
            IsPlayer = isPlayer;
            Position = position;
        }

        public static CharacterModel CreatePlayer()
        {
            return new CharacterModel(
                isPlayer: true,
                position: 0);
        }

        public static CharacterModel CreateEnemy(int index)
        {
            return new CharacterModel(
                isPlayer: false,
                position: index);
        }
    }
}
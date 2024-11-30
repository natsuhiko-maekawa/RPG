namespace BattleScene.Framework.ViewModel
{
    public struct CharacterStruct
    {
        public bool IsPlayer { get; }
        public int Position { get; }

        private CharacterStruct(
            bool isPlayer,
            int position)
        {
            IsPlayer = isPlayer;
            Position = position;
        }

        public static CharacterStruct CreatePlayer()
        {
            return new CharacterStruct(
                isPlayer: true,
                position: 0);
        }

        public static CharacterStruct CreateEnemy(int index)
        {
            return new CharacterStruct(
                isPlayer: false,
                position: index);
        }
    }
}
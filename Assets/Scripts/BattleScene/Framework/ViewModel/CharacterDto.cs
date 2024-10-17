namespace BattleScene.Framework.ViewModel
{
    public class CharacterDto
    {
        public bool IsPlayer { get; }
        public int EnemyIndex { get; }

        private CharacterDto()
        {
            IsPlayer = true;
        }

        public CharacterDto(
            int enemyIndex)
        {
            EnemyIndex = enemyIndex;
        }

        public static CharacterDto CreatePlayer()
        {
            return new CharacterDto();
        }
    }
}
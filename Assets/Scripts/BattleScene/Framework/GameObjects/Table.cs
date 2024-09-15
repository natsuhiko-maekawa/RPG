namespace BattleScene.Framework.GameObjects
{
    public class Table : Grid<Row>
    {
        private void Awake()
        {
            Initialize();
        }
    }
}
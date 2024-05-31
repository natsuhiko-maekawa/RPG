using System;

namespace BattleScene.Domain.Entity
{
    public class SelectorEntity
    {
        public SelectorEntity(
            int maxViewLength,
            int listLength)
        {
            ActualViewLength = Math.Min(maxViewLength, listLength);
            UpperLimit = listLength - ActualViewLength;
        }

        public int ActualViewLength { get; private set; }
        public int UpperLimit { get; private set; }
        public int Selection { get; private set; }
        public int ListStart { get; private set; }

        [Obsolete]
        public void Initialize(int maxViewLength, int listLength)
        {
            ActualViewLength = Math.Min(maxViewLength, listLength);
            UpperLimit = listLength - ActualViewLength;
        }

        public void Reset()
        {
            Selection = default;
        }

        public void Up()
        {
            ListStart = 0 < Selection ? ListStart : --ListStart;
            ListStart = ListStart <= 0 ? 0 : ListStart;
            Selection = Selection <= 0 ? 0 : --Selection;
        }

        public void Down()
        {
            ListStart = Selection < ActualViewLength - 1
                ? ListStart
                : ++ListStart;
            ListStart = UpperLimit <= ListStart
                ? UpperLimit
                : ListStart;
            Selection = ActualViewLength - 1 <= Selection
                ? ActualViewLength - 1
                : ++Selection;
        }

        public void Left()
        {
            Up();
        }

        public void Right()
        {
            Down();
        }
    }
}
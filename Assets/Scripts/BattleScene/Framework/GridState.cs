using System;

namespace BattleScene.Framework
{
    public class GridState
    {
        public GridState(
            int maxGridSize,
            int itemCount)
        {
            GridSize = Math.Min(maxGridSize, itemCount);
            MaxIndex = itemCount - GridSize;
        }

        public int GridSize { get; private set; }
        public int MaxIndex { get; private set; }
        public int SelectedRow { get; private set; }
        public int TopItemIndex { get; private set; }

        public bool IsHiddenUpper => TopItemIndex > 0;
        public bool IsHiddenLower => TopItemIndex < MaxIndex;
        
        public void Reset()
        {
            SelectedRow = 0;
        }

        public void Up()
        {
            TopItemIndex = 0 < SelectedRow 
                ? TopItemIndex
                : --TopItemIndex;
            TopItemIndex = TopItemIndex <= 0 
                ? 0 
                : TopItemIndex;
            SelectedRow = SelectedRow <= 0 
                ? 0 
                : --SelectedRow;
        }

        public void Down()
        {
            TopItemIndex = SelectedRow < GridSize - 1
                ? TopItemIndex
                : ++TopItemIndex;
            TopItemIndex = MaxIndex <= TopItemIndex
                ? MaxIndex
                : TopItemIndex;
            SelectedRow = GridSize - 1 <= SelectedRow
                ? GridSize - 1
                : ++SelectedRow;
        }
    }
}
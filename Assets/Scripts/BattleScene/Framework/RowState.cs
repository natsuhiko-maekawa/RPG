using System;

namespace BattleScene.Framework
{
    public class RowState
    {
        public RowState(
            int maxTableSize,
            int itemCount)
        {
            _tableSize = Math.Min(maxTableSize, itemCount);
            _maxIndex = itemCount - _tableSize;
        }

        private readonly int _tableSize;
        private readonly int _maxIndex;
        public int SelectedRow { get; private set; }
        public int TopItemIndex { get; private set; }

        public int SelectedIndex => TopItemIndex + SelectedRow;
        public bool IsHiddenUpper => TopItemIndex > 0;
        public bool IsHiddenLower => TopItemIndex < _maxIndex;

        // TODO: 三項演算子である意味がない。可読性を上げるため、if文に書き換えること。
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
            TopItemIndex = SelectedRow < _tableSize - 1
                ? TopItemIndex
                : ++TopItemIndex;
            TopItemIndex = _maxIndex <= TopItemIndex
                ? _maxIndex
                : TopItemIndex;
            SelectedRow = _tableSize - 1 <= SelectedRow
                ? _tableSize - 1
                : ++SelectedRow;
        }
    }
}
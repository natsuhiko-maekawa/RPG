using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene.Framework.GameObjects
{
    public class Grid : MonoBehaviour, IEnumerable<Row>
    {
        private Row _row;
        private readonly List<Row> _rowList = new();
        public Row this[int index] => _rowList[index];

        private void Awake()
        {
            _row = GetComponentInChildren<Row>();
            _rowList.Add(_row);
            enabled = false;
        }
        
        public void SetRow(int rowCount)
        {
            enabled = true;
            if (rowCount > _rowList.Count)
            {
                AddText(rowCount - _rowList.Count);
                return;
            }

            if (rowCount < _rowList.Count) 
                RemoveText(_rowList.Count - rowCount);
        }

        public void Reset()
        {
            RemoveText(_rowList.Count);
        }

        private void AddText(int rowCount)
        {
            for (var i = 0; i < rowCount; ++i)
            {
                var row = Instantiate(_row, transform);
                row.name = _row.name;
                _rowList.Add(row);
                _rowList[i].Hide();
            }
        }

        private void RemoveText(int rowCount)
        {
            for (var i = _rowList.Count - 1; i > rowCount; --i)
            {
                Destroy(_rowList[i]);
                _rowList.RemoveAt(i);
            }
        }

        public IEnumerator<Row> GetEnumerator()
        {
            return _rowList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
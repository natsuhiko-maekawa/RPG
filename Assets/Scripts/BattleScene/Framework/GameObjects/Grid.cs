using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        }
        
        public void SetRow(int rowCount)
        {
            if (rowCount > _rowList.Count)
            {
                AddText(rowCount - _rowList.Count);
                return;
            }

            if (rowCount < _rowList.Count) 
                RemoveText(_rowList.Count - rowCount);
        }

        public void ResetRow()
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
            var max = Math.Min(rowCount, _rowList.Count);
            for (var i = 0; i < max; ++i)
            {
                Destroy(_rowList.Last().gameObject);
                _rowList.RemoveAt(_rowList.Count - 1);
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
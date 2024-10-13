using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleScene.Framework.GameObjects
{
    public class Grid<TGameObject> : MonoBehaviour, IEnumerable<TGameObject> where TGameObject : MonoBehaviour
    {
        private TGameObject _gameObject;
        private readonly List<TGameObject> _gameObjectList = new();
        public TGameObject this[int index] => _gameObjectList[index];
        
        public void SetItem(int itemCount)
        {
            _gameObject = GetComponentInChildren<TGameObject>();
            _gameObjectList.Add(_gameObject);
            
            if (itemCount > _gameObjectList.Count)
            {
                AddItem(itemCount - _gameObjectList.Count);
                return;
            }

            if (itemCount < _gameObjectList.Count) 
                RemoveItem(_gameObjectList.Count - itemCount);
        }

        public void ResetItem()
        {
            RemoveItem(_gameObjectList.Count - 1);
        }

        public void SetActive() => gameObject.SetActive(true);
        public void SetInActive() => gameObject.SetActive(false);

        private void AddItem(int itemCount)
        {
            for (var i = 0; i < itemCount; ++i)
            {
                var row = Instantiate(_gameObject, transform);
                row.name = _gameObject.name;
                _gameObjectList.Add(row);
            }
        }

        private void RemoveItem(int itemCount)
        {
            var max = Math.Min(itemCount, _gameObjectList.Count);
            for (var i = 0; i < max; ++i)
            {
                Destroy(_gameObjectList.Last().gameObject);
                _gameObjectList.RemoveAt(_gameObjectList.Count - 1);
            }
        }

        public IEnumerator<TGameObject> GetEnumerator()
        {
            return _gameObjectList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
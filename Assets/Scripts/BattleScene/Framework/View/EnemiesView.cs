using System.Collections;
using System.Collections.Generic;
using BattleScene.Framework.GameObjects;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class EnemiesView : MonoBehaviour, IEnumerable<EnemyView>
    {
        public int maxCacheSize = 4;
        private EnemyColumn _enemyViewColumn;

        private void Awake()
        {
            _enemyViewColumn = GetComponent<EnemyColumn>();
            _enemyViewColumn.SetItem(maxCacheSize);
            enabled = false;
        }

        private void OnEnable()
        {
            foreach (var enemyView in _enemyViewColumn) enemyView.enabled = true;
        }

        private void OnDisable()
        {
            foreach (var enemyView in _enemyViewColumn) enemyView.enabled = false;
        }

        public EnemyView this[int i] => _enemyViewColumn[i];
        public IEnumerator<EnemyView> GetEnumerator() => _enemyViewColumn.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
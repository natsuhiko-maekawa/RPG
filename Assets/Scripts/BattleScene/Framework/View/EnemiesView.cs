using System.Collections;
using System.Collections.Generic;
using BattleScene.Framework.GameObjects;
using BattleScene.InterfaceAdapter.Interface;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class EnemiesView : MonoBehaviour, IEnemiesView, IEnumerable<EnemyView>
    {
        [SerializeField] private int maxCacheSize = 4;
        private EnemyView _enemyView;
        private EnemyGrid _enemyViewGrid;

        private void Awake()
        {
            _enemyViewGrid = GetComponent<EnemyGrid>();
            _enemyViewGrid.Initialize();
            _enemyViewGrid.SetItem(maxCacheSize);
        }
        
        public void StopEnemyFrameView()
        {
            foreach (var enemyView in _enemyViewGrid) enemyView.StopFrameAnimation();
        }

        public IEnemyView this[int i] => _enemyViewGrid[i];

        public IEnumerator<EnemyView> GetEnumerator()
        {
            return _enemyViewGrid.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
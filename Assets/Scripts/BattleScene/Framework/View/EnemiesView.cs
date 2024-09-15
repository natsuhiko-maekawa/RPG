using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
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

        [Obsolete]
        public Task StartEnemyFrameView(EnemyFrameViewDto dto)
        {
            return Task.CompletedTask;
        }

        [Obsolete]
        public void StopEnemyFrameView()
        {
        }

        [Obsolete]
        public Task StartEnemyVibesView(EnemyVibesViewDto dto)
        {
            return Task.CompletedTask;
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
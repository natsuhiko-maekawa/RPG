using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.EnemyView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class EnemiesView : MonoBehaviour, IEnemiesView, IEnumerable<EnemyView>
    {
        [SerializeField] private GameObject enemyView;
        [SerializeField] private int maxCacheSize = 4;
        [SerializeField] private float enemyIntervalPx = 200;
        private EnemyView _enemyView;
        private readonly List<EnemyView> _enemyViewList = new();
        private EnemyGrid _enemyViewGrid;

        private void Awake()
        {
            // _enemyView = GetComponentInChildren<EnemyView>();
            // for (var i = 0; i < maxCacheSize; ++i)
            // {
            //     var enemyViewScript = Instantiate(_enemyView, transform);
            //     Debug.Assert(enemyViewScript != null);
            //     enemyViewScript.SetActive(false);
            //     _enemyViewList.Add(enemyViewScript);
            // }

            _enemyViewGrid = GetComponent<EnemyGrid>();
            _enemyViewGrid.Initialize();
            _enemyViewGrid.SetItem(maxCacheSize);
        }

        [Obsolete]
        public Task StartEnemyAilmentsView(EnemyAilmentsViewDto dto)
        {
            return Task.CompletedTask;
        }
        
        [Obsolete]
        public Task StartEnemyDigitView(EnemyDigitViewDto dto)
        {
            return Task.CompletedTask;
        }

        [Obsolete]
        public Task StartEnemyHpBarView(EnemyHpBarViewDto dto)
        {
            return Task.CompletedTask;
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

        public async Task StartEnemyView(EnemyViewDto dto)
        {
            // foreach (var (enemyViewInstance, index) in _enemyViewList.Select((x, i) => (x, i)))
            // {
            //     if (!dto.EnemyDtoList.Select(x => x.EnemyNumber).Contains(index)) continue;
            //     
            //     enemyViewInstance.SetActive(true);
            //     var enemyImagePath = dto.EnemyDtoList.First(y => y.EnemyNumber == index).EnemyImagePath;
            //     var sprite = await _spriteFlyweight.Get(enemyImagePath);
            //     enemyViewInstance.GetComponent<Image>().sprite = sprite;
            //     var vector3 = new Vector3(GetX(dto.EnemyCount, index), 0.0f);
            //     enemyViewInstance.transform.localPosition += vector3;
            // }
        }

        private float GetX(int enemyCount, int enemyPosition)
        {
            return Enumerable.Range(0, enemyCount)
                .Select(x => (enemyCount - 1) * -enemyIntervalPx / 2 + x * enemyIntervalPx)
                .Where((_, i) => i == enemyPosition)
                .First();
        }

        public IEnumerator<EnemyView> GetEnumerator()
        {
            return _enemyViewList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
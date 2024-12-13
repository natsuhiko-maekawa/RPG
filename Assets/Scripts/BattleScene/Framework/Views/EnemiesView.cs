using System.Collections;
using System.Collections.Generic;
using BattleScene.Framework.GameObjects;
using UnityEngine;

namespace BattleScene.Framework.Views
{
    public class EnemiesView : MonoBehaviour, IEnumerable<EnemyView>
    {
        private EnemyGroup _enemyViewGroup;

        private void Awake()
        {
            _enemyViewGroup = GetComponent<EnemyGroup>();
            enabled = false;
        }

        private void OnEnable()
        {
            foreach (var enemyView in _enemyViewGroup) enemyView.enabled = true;
        }

        public void StartAnimation(int enemyCount)
        {
            _enemyViewGroup.SetItem(enemyCount);
            enabled = true;
        }

        private void OnDisable()
        {
            foreach (var enemyView in _enemyViewGroup) enemyView.enabled = false;
        }

        public EnemyView this[int i] => _enemyViewGroup[i];
        public IEnumerator<EnemyView> GetEnumerator() => _enemyViewGroup.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
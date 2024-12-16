using System;
using System.Collections;
using System.Collections.Generic;
using BattleScene.Views.GameObjects;
using UnityEngine;

namespace BattleScene.Views.Views
{
    public class EnemyGroupView : MonoBehaviour, IEnumerable<EnemyView>
    {
        private EnemyViewGroup _enemyViewGroup;

        private void Awake()
        {
            _enemyViewGroup = GetComponent<EnemyViewGroup>();
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

        public EnemyView this[Index i] => _enemyViewGroup[i];
        public IEnumerator<EnemyView> GetEnumerator() => _enemyViewGroup.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
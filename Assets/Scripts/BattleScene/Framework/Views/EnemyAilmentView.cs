using System.Collections.Generic;
using System.Linq;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.Utilities;
using BattleScene.Framework.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.Views
{
    public class EnemyAilmentView : MonoBehaviour
    {
        [SerializeField] private int iconPoolSize = 8;
        [SerializeField] private Image icon;
        [SerializeField] private Texture2D icons;
        private readonly List<int> _ailmentIdList = new();
        private Sprite[] _iconArray;
        private EnemyAilmentGrid _enemyAilmentGrid;

        private void Awake()
        {
            _enemyAilmentGrid = GetComponent<EnemyAilmentGrid>();
            _enemyAilmentGrid.SetItem(iconPoolSize);
            // TODO: 各ViewでiconArrayを作成しているため、修正すること。
            _iconArray = MySprite.CreateByGrid(icons, 4, 4);
        }

        public void StartAnimation(AilmentViewModel ailment)
        {
            if (ailment.Effects && !_ailmentIdList.Contains(ailment.AilmentId)) _ailmentIdList.Add(ailment.AilmentId);
            if (!ailment.Effects) _ailmentIdList.Remove(ailment.AilmentId);

            foreach (var (enemyAilment, i) in _enemyAilmentGrid.Select((x, i) => (x, i)))
            {
                if (i < _ailmentIdList.Count)
                {
                    var ailmentId = _ailmentIdList[i];
                    var sprite = _iconArray[ailmentId];
                    enemyAilment.SetIcon(sprite);
                }
                else
                {
                    enemyAilment.ResetIcon();
                }
            }
        }
    }
}
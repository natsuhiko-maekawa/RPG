using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Framework.Utility;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace BattleScene.Framework.View
{
    public class EnemyAilmentsView : MonoBehaviour
    {
        private const int AreaWidth = 95;
        private const int AilmentsSmallIconSize = 13;
        private const int MaxIconNum = 8;
        [SerializeField] private Image icon;
        [SerializeField] private Texture2D icons;
        private readonly SortedSet<int> _ailmentIdSet = new();
        private readonly List<Image> _enemyAilmentsIconList = new();
        private Sprite[] _iconArray;

        private void Awake()
        {
            const float left = -AreaWidth / 2.0f;
            for (var i = 0; i < MaxIconNum; ++i)
            {
                var image = Instantiate(icon, transform);
                image.transform.localPosition += new Vector3(left + AilmentsSmallIconSize * i, 60, 0);
                image.enabled = false;
                _enemyAilmentsIconList.Add(image);
            }

            _iconArray = MySprite.CreateByGrid(icons, 4, 4);
        }

        public Task StartAnimation(AilmentViewModel dto)
        {
            // TODO: 本当はHashSetの末尾に要素を追加したい
            if (dto.Effects) _ailmentIdSet.Add(dto.AilmentId);
            else _ailmentIdSet.Remove(dto.AilmentId);
            
            foreach (var enemyAilmentsIcon in _enemyAilmentsIconList) enemyAilmentsIcon.enabled = false;

            foreach (var (ailmentId, index) in _ailmentIdSet.Select((x, i) => (x, i)))
            {
                _enemyAilmentsIconList[index].sprite = _iconArray[ailmentId];
                _enemyAilmentsIconList[index].enabled = true;
            }

            return Task.CompletedTask;
        }
    }
}
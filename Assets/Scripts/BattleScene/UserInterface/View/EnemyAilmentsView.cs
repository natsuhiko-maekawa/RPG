using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace BattleScene.UserInterface.View
{
    public class EnemyAilmentsView : MonoBehaviour
    {
        private const int AreaWidth = 95;
        private const int AilmentsSmallIconSize = 13;
        private const int MaxIconNum = 8;
        [SerializeField] private Image icon;
        [SerializeField] private Texture2D icons;
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

            _iconArray = SpriteEx.CreateByGrid(icons, 4, 4);
        }

        public Task StartAnimation(IList<AilmentsDto> dtoList)
        {
            foreach (var enemyAilmentsIcon in _enemyAilmentsIconList) enemyAilmentsIcon.enabled = false;

            foreach (var (iconNum, index) in dtoList.Select((x, i) => (Ailments: x.AilmentsInt, i)))
            {
                _enemyAilmentsIconList[index].sprite = _iconArray[iconNum];
                _enemyAilmentsIconList[index].enabled = true;
            }

            return Task.CompletedTask;
        }
    }
}
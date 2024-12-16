using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Views.GameObjects;
using BattleScene.Views.Utilities;
using BattleScene.Views.ViewModels;
using Common;
using UnityEngine;

namespace BattleScene.Views.Views
{
    public class OrderView : MonoBehaviour
    {
        [SerializeField] private Sprite player;
        [SerializeField] private Texture2D ailmentsIconTexture;
        private Order _order;
        private Sprite[] _ailmentsIconArray;
        private readonly Dictionary<string, Sprite> _enemyImageDictionary = new();

        private void Awake()
        {
            _ailmentsIconArray = MySprite.CreateByGrid(ailmentsIconTexture, 4, 4);
            _order = GetComponentInChildren<Order>();
            _order.SetItem(Constant.MaxOrderCount);
            _order.enabled = true;
        }

        public async Task Initialize(string[] enemyImagePathArray)
        {
            var spriteFlyweight = SpriteFlyweight.Instance;
            foreach (var enemyImagePath in enemyImagePathArray)
            {
                var sprite = await spriteFlyweight.GetAsync(enemyImagePath);
                _enemyImageDictionary.Add(enemyImagePath, sprite);
            }
        }

        public void StartAnimation(IReadOnlyList<OrderViewModel> modelList)
        {
            foreach (var (orderIcon, model) in _order
                         .Zip(modelList, (x, y) => (orderIcon: x, model: y)))
            {
                var sprite = model.ItemType switch
                {
                    ItemType.Player => player,
                    ItemType.Enemy => _enemyImageDictionary[model.EnemyImagePath],
                    ItemType.Ailment => _ailmentsIconArray[model.AilmentInt],
                    _ => throw new ArgumentOutOfRangeException()
                };

                orderIcon.Set(sprite);
                orderIcon.enabled = true;
                _order.Slide();
            }
        }
    }
}
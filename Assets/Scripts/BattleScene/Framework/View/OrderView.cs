using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Framework.Utility;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using static BattleScene.Framework.Constant;

namespace BattleScene.Framework.View
{
    public class OrderView : MonoBehaviour
    {
        private const int MaxOrderNumber = 14;
        private const int IconWidth = 50;
        private const int Centering = IconWidth * MaxOrderNumber / 2 - IconWidth / 2;
        private const int Frame = 10;
        [SerializeField] private Image icon;
        [SerializeField] private Sprite player;
        [SerializeField] private Texture2D ailmentsIconTexture;
        private readonly List<Image> _imageList = new();
        private Sprite[] _ailmentsIconArray;
        private float _defaultX;
        private SpriteFlyweight _spriteFlyweight;

        private void Awake()
        {
            for (var i = 0; i < MaxOrderNumber; i++)
            {
                _imageList.Add(Instantiate(icon, transform));
                var iconPosition = _imageList[i].transform.localPosition;
                iconPosition.x += i * IconWidth - Centering;
                _imageList[i].transform.localPosition = iconPosition;
                _imageList[i].enabled = false;
            }

            _defaultX = _imageList.First().transform.localPosition.x;
            _ailmentsIconArray = MySprite.CreateByGrid(ailmentsIconTexture, 4, 4);
            _spriteFlyweight = SpriteFlyweight.Instance;
        }

        public async Task StartAnimationAsync(IList<OrderViewDto> dtoList)
        {
            float diff = 0;

            for (var frame = 0; frame < Frame; ++frame)
            {
                _imageList.First().enabled = false;
                var sin = Mathf.Sin(frame / (float)Frame * 90 * Mathf.Deg2Rad);
                sin -= diff;
                foreach (var image in _imageList)
                {
                    var imageTransform = image.transform;
                    var imagePosition = imageTransform.localPosition;
                    imagePosition.x += sin * -IconWidth;
                    imageTransform.localPosition = imagePosition;
                }

                diff += sin;
                await Task.Delay(WaitTime);
            }

            _imageList.First().enabled = true;
            foreach (var (dto, i) in dtoList.Select((x, i) => (x, i)))
            {
                var iconPosition = _imageList[i].transform.localPosition;
                iconPosition.x = _defaultX;
                iconPosition.x += i * IconWidth;
                _imageList[i].transform.localPosition = iconPosition;
                _imageList[i].sprite = dto.ItemType switch
                {
                    ItemType.Player => player,
                    ItemType.Enemy when dto.EnemyImagePath != null => await _spriteFlyweight.GetAsync(
                        dto.EnemyImagePath),
                    ItemType.Ailment when dto.AilmentNumber != null => _ailmentsIconArray[(int)dto.AilmentNumber],
                    _ => throw new ArgumentOutOfRangeException()
                };

                _imageList[i].enabled = true;
            }
        }
    }
}
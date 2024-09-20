using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static BattleScene.Framework.Constant;
using Random = UnityEngine.Random;

namespace BattleScene.Framework.View
{
    public class EnemyVibesView : MonoBehaviour
    {
        private const int Frame = 10;
        private const float VibesRange = 5;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public async Task StartAnimation()
        {
            var originalPosition = _image.transform.localPosition;
            for (var frame = 0; frame < Frame; ++frame)
            {
                var x = Random.Range(-VibesRange, VibesRange);
                var y = Random.Range(-VibesRange, VibesRange);
                var move = new Vector3(x, y, 0);
                _image.transform.localPosition = originalPosition + move;
                await Task.Delay(WaitTime);
            }

            _image.transform.localPosition = originalPosition;
        }
    }
}
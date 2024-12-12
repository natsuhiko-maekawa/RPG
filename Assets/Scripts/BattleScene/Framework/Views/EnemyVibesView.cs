using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static BattleScene.Framework.Constant;
using Random = UnityEngine.Random;

namespace BattleScene.Framework.Views
{
    public class EnemyVibesView : MonoBehaviour
    {
        [SerializeField] private int frame = 10;
        [SerializeField] private float vibesRange = 5;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public async Task StartAnimationAsync()
        {
            var originalPosition = _image.transform.localPosition;
            for (var i = 0; i < frame; ++i)
            {
                var x = Random.Range(-vibesRange, vibesRange);
                var y = Random.Range(-vibesRange, vibesRange);
                var move = new Vector3(x, y, 0);
                _image.transform.localPosition = originalPosition + move;
                await Task.Delay(WaitTime);
            }

            _image.transform.localPosition = originalPosition;
        }
    }
}
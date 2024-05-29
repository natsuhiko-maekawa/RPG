using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using UnityEngine;
using UnityEngine.UI;
using static BattleScene.UserInterface.Constant;
using Random = UnityEngine.Random;

namespace BattleScene.UserInterface.View
{
    public class PlayerVibesView : MonoBehaviour, IPlayerVibesView
    {
        private const int Frame = 10;
        private const float VibesRange = 5;
        private Image _image;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _originalPosition = _image.transform.localPosition;
        }

        public async Task StartAnimation()
        {
            for (var frame = 0; frame < Frame; ++frame)
            {
                var move = new Vector3(Random.Range(-VibesRange, VibesRange), Random.Range(-VibesRange, VibesRange), 0);
                _image.transform.localPosition = _originalPosition + move;
                await Task.Delay(WaitTime);
            }

            _image.transform.localPosition = _originalPosition;
        }
    }
}
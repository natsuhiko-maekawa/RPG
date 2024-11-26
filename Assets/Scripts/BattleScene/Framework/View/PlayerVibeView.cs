using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class PlayerVibeView : MonoBehaviour
    {
        [HideInInspector] public bool vibe;
        public float vibesIntervalSecond = 0.03f;
        public float vibesRangePx = 5;
        private Image _image;
        private Transform _imageTransform;
        private Vector3 _originalPosition;
        private float _frameRate;
        private int _frame = -1;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _imageTransform = _image.transform;
            _originalPosition = _image.transform.localPosition;
            _frameRate = Application.targetFrameRate;
        }

        private void Update()
        {
            ++_frame;
            if (_frame == 0 && !vibe)
            {
                _image.transform.localPosition = _originalPosition;
                return;
            }

            if (!vibe) return;

            if (_frame + 1 > _frameRate * vibesIntervalSecond)
            {
                var x = Random.Range(-vibesRangePx, vibesRangePx);
                var y = Random.Range(-vibesRangePx, vibesRangePx);
                var move = new Vector3(x, y, 0);
                _imageTransform.localPosition = _originalPosition + move;
                _frame = -1;
            }
        }
    }
}
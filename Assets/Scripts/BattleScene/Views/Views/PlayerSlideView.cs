using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.Views
{
    public class PlayerSlideView : MonoBehaviour
    {
        [HideInInspector] public float x;
        private Image _image;
        private Transform _imageTransform;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _imageTransform = _image.transform;
            _originalPosition = _image.transform.localPosition;
        }

        private void Update()
        {
            if (x == 0) return;
            _imageTransform.localPosition = _originalPosition + new Vector3(x, 0, 0);
        }
    }
}
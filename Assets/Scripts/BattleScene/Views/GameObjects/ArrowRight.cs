using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.GameObjects
{
    public class ArrowRight : MonoBehaviour
    {
        [SerializeField] private int rowHeight;
        private Image _image;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _originalPosition = _image.rectTransform.localPosition;
            _image.enabled = false;
        }

        public void Move(int row)
        {
            enabled = true;
            _image.enabled = true;
            var newPosition = _originalPosition + new Vector3(0, -rowHeight * row);
            _image.rectTransform.localPosition = newPosition;
        }

        private void OnDisable()
        {
            _image.enabled = false;
        }
    }
}
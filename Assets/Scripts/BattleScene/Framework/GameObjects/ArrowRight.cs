using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.GameObjects
{
    public class ArrowRight : MonoBehaviour
    {
        [SerializeField] private int rowHeight;
        private Image _rightArrow;
        private Vector3 _defaultPosition;

        private void Awake()
        {
            _rightArrow = GetComponent<Image>();
            _defaultPosition = _rightArrow.rectTransform.localPosition;
            enabled = false;
        }

        public void Move(int row)
        {
            enabled = true;
            var newPosition = _defaultPosition + new Vector3(0, -rowHeight * row);
            _rightArrow.rectTransform.localPosition = newPosition;
        }
    }
}
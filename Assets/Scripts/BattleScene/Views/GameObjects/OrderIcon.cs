using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.GameObjects
{
    public class OrderIcon : MonoBehaviour
    {
        private Image _icon;

        private void Awake()
        {
            _icon = GetComponent<Image>();
        }

        private void OnEnable()
        {
            _icon.enabled = true;
        }

        public void Set(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        private void OnDisable()
        {
            _icon.enabled = false;
        }
    }
}
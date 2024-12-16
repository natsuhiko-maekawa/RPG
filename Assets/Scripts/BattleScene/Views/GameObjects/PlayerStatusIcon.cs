using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.GameObjects
{
    public class PlayerStatusIcon : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Set(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        private void OnEnable()
        {
            _image.enabled = true;
        }

        private void OnDisable()
        {
            _image.enabled = false;
        }
    }
}
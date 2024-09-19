using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.GameObjects
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

        public void Activate()
        {
            _image.enabled = true;
        }

        public void Inactivate()
        {
            _image.enabled = false;
        }
    }
}
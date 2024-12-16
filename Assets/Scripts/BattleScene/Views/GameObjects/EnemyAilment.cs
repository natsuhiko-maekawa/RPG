using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.GameObjects
{
    public class EnemyAilment : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.enabled = false;
        }

        public void SetIcon(Sprite sprite)
        {
            _image.sprite = sprite;
            _image.enabled = true;
        }

        public void ResetIcon()
        {
            _image.enabled = false;
        }
    }
}
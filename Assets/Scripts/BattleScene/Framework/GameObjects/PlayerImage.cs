using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.GameObjects
{
    public class PlayerImage : MonoBehaviour
    {
        private Image _image;
        private Text _text;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<Text>();
        }

        public void Set(Sprite sprite) => _image.sprite = sprite;

        public void Show()
        {
            _image.enabled = true;
            _text.enabled = false;
        }

        public void Hide() => _image.enabled = false;
        public void SetText(string text) => _text.text = text;
        public void ShowText() => _text.enabled = true;
    }
}
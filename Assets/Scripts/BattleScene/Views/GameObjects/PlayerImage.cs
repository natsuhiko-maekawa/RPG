using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.GameObjects
{
    public class PlayerImage : MonoBehaviour
    {
        private Image _image;
        private Animator _animator;
        private Text _text;
        private bool _hasImage;
        private static readonly int SlideTrigger = Animator.StringToHash("Slide");
        private static readonly int VibeTrigger = Animator.StringToHash("Vibe");

        private void Awake()
        {
            _image = GetComponent<Image>();
            _animator = GetComponent<Animator>();
            _text = GetComponentInChildren<Text>();
            _text.text = "NoImage";
        }

        private void OnEnable()
        {
            _image.enabled = true;
            if (!_hasImage)
            {
                _image.enabled = false;
                _text.enabled = true;
            }
        }

        public void Set(Sprite sprite)
        {
            _image.sprite = sprite;
            _hasImage = true;
        }

        public void IsNothing()
        {
            _hasImage = false;
        }

        public void Slide() => _animator.SetTrigger(SlideTrigger);
        public void Vibe() => _animator.SetTrigger(VibeTrigger);

        public void OnDisable()
        {
            _image.enabled = false;
            _text.enabled = false;
        }
    }
}
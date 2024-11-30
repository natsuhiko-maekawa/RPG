using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.GameObjects
{
    public class PlayerImage : MonoBehaviour
    {
        private Image _image;
        private Animator _animator;
#if UNITY_EDITOR
        private Text _text;
        private bool _hasImage;
#endif
        private static readonly int SlideTrigger = Animator.StringToHash("Slide");
        private static readonly int VibeTrigger = Animator.StringToHash("Vibe");

        private void Awake()
        {
            _image = GetComponent<Image>();
            _animator = GetComponent<Animator>();
#if UNITY_EDITOR
            _text = GetComponentInChildren<Text>();
            _text.text = "NoImage";
#endif
        }

        private void OnEnable()
        {
            _image.enabled = true;
#if UNITY_EDITOR
            if (!_hasImage)
            {
                _image.enabled = false;
                _text.enabled = true;
            }
#endif
        }

        public void Set(Sprite sprite)
        {
            _image.sprite = sprite;
#if UNITY_EDITOR
            _hasImage = true;
#endif
        }

#if UNITY_EDITOR
        public void IsNothing()
        {
            _hasImage = false;
        }
#endif

        public void Slide() => _animator.SetTrigger(SlideTrigger);
        public void Vibe() => _animator.SetTrigger(VibeTrigger);

        public void OnDisable()
        {
            _image.enabled = false;
            _text.enabled = false;
        }
    }
}
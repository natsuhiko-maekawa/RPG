using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.GameObjects
{
    public class EnemyImage : MonoBehaviour
    {
        private Image _image;
        private Animator _animator;
        private static readonly int VibeTrigger = Animator.StringToHash("Vibe");

        private void Awake()
        {
            _image = GetComponent<Image>();
            _animator = GetComponent<Animator>();
        }

        public void Set(Sprite sprite) => _image.sprite = sprite;

        private void OnEnable()
        {
            _image.enabled = true;
        }

        public void OnDisable()
        {
            _image.enabled = false;
        }

        public void Vibe() => _animator.SetTrigger(VibeTrigger);
    }
}
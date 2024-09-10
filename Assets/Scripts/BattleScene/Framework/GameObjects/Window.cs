using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.GameObjects
{
    public class Window : MonoBehaviour
    {
        private Animator _animator;
        private Image _window;
        private static readonly int ShowTrigger = Animator.StringToHash("Show");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _window = GetComponent<Image>();
            _window.enabled = false;
        }

        public void Show()
        {
            _window.enabled = true;
            _animator.SetTrigger(ShowTrigger);
        }

        public void Hide()
        {
            _window.enabled = false;
        }
    }
}
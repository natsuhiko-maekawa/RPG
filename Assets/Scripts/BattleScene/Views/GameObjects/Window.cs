using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.GameObjects
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

        private void OnEnable()
        {
            _animator.SetTrigger(ShowTrigger);
            _window.enabled = true;
        }

        private void OnDisable()
        {
            _window.enabled = false;
        }
    }
}
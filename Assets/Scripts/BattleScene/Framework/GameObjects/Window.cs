using UnityEngine;

namespace BattleScene.Framework.GameObjects
{
    public class Window : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int ShowTrigger = Animator.StringToHash("Show");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            enabled = false;
        }

        public void Show()
        {
            enabled = true;
            _animator.SetTrigger(ShowTrigger);
        }
    }
}
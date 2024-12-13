using UnityEngine;

namespace BattleScene.Framework.GameObjects
{
    public class Order : Group<OrderIcon>
    {
        private Animator _animator;
        private static readonly int SlideTrigger = Animator.StringToHash("Slide");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Slide() => _animator.SetTrigger(SlideTrigger);
    }
}
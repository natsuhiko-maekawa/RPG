using BattleScene.Framework.GameObjects;
using UnityEngine;

namespace BattleScene.Framework.StateMachineBehaviours
{
    public class OrderSlideAnimationBehaviour : StateMachineBehaviour
    {
        private Order _order;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _order = animator.transform.gameObject.GetComponent<Order>();
            _order[^1].enabled = false;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _order[^1].enabled = true;
        }
    }
}
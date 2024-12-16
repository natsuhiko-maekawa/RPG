using BattleScene.Views.Views;
using TMPro;
using UnityEngine;

namespace BattleScene.Views.StateMachineBehaviours
{
    public class MessageAnimationBehaviour : StateMachineBehaviour
    {
        private MessageView _messageView;
        private TMP_Text _tmpText;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var gameObject = animator.transform.gameObject;
            _messageView = gameObject.GetComponent<MessageView>();
            _tmpText = gameObject.GetComponentInChildren<TMP_Text>();
            _tmpText.maxVisibleCharacters = 0;
            _tmpText.enabled = true;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_tmpText.maxVisibleCharacters == _messageView.maxVisibleCharacters) return;
            _tmpText.maxVisibleCharacters = _messageView.maxVisibleCharacters;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _tmpText.enabled = false;
        }
    }
}
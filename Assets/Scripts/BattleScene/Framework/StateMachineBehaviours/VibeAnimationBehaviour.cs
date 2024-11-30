using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleScene.Framework.StateMachineBehaviours
{
    public class VibeAnimationBehaviour : StateMachineBehaviour
    {
        public float vibesIntervalSecond;
        public float vibesRangePx;
        private GameObject _gameObject;
        private Transform _transform;
        private Vector3 _originalPosition;
        private float _frameRate;
        private int _frame = -1;

        private void Awake()
        {
            _frameRate = Application.targetFrameRate;
        }

        private void Reset()
        {
            vibesIntervalSecond = 0.03f;
            vibesRangePx = 5;
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _gameObject = animator.transform.gameObject;
            _transform = _gameObject.transform;
            _originalPosition = _gameObject.transform.localPosition;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vibe();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _gameObject.transform.localPosition = _originalPosition;
        }

        private void Vibe()
        {
            ++_frame;
            if (_frame + 1 > _frameRate * vibesIntervalSecond)
            {
                var x = Random.Range(-vibesRangePx, vibesRangePx);
                var y = Random.Range(-vibesRangePx, vibesRangePx);
                var move = new Vector3(x, y, 0);
                _transform.localPosition = _originalPosition + move;
                _frame = -1;
            }
        }
    }
}
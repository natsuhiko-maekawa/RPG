using System.Linq;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.ViewModel;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class DigitView : MonoBehaviour
    {
        private const int PoolSize = 10;
        [SerializeField] private int frame = 20;
        [SerializeField] private float moveRange = 20.0f;
        [SerializeField] private float randomRange = 80.0f;
        public float digitsIntervalSecond = 0.15f;

        // ReSharper disable once RedundantDefaultMemberInitializer
        [SerializeField] private float alphaRate = 0.0f;
        [SerializeField] private int waitTime = 70;
        [SerializeField] private string avoidText = "avoid";

        [SerializeField] private bool isPlayer;
        private Digit _digit;
        private readonly Digit[] _digitPool = new Digit[PoolSize];
        private DigitListViewModel _model;
        private bool _animation;
        private float _frameRate;
        private int _frame = -1;
        private int _index = -1;

        private void Awake()
        {
            for (var i = 0; i < _digitPool.Length; ++i)
            {
                if (_digit is null)
                {
                    _digit = GetComponentInChildren<Digit>();
                    _digitPool[i] = _digit;
                }
                else
                {
                    var text = Instantiate(_digit, transform);
                    _digitPool[i] = text;
                }

                _digitPool[i].enabled = false;
            }

            _frameRate = Application.targetFrameRate;
        }

        public void StartAnimation(DigitListViewModel model)
        {
            foreach (var digit in _digitPool)
            {
                digit.ResetDigit();
            }

            model.DigitList
                .Aggregate(_digitPool, (digitPool, digit) =>
                {
                    SetDigit(digitPool, digit);
                    return digitPool;
                });

            _animation = true;
        }

        private void Update()
        {
            if (!_animation) return;

            ++_frame;
            if (_frame + 1 > _frameRate * digitsIntervalSecond)
            {
                ++_index;
                _digitPool[_index].enabled = true;

                var rand = isPlayer ? Mathf.Floor(Random.Range(-randomRange, randomRange) / 10) * 10.0f : 0;
                _digitPool[_index].SetPosition(new Vector3(rand, rand, 0));

                if (_index + 1 >= _digitPool.Length)
                {
                    _index = -1;
                    _animation = false;
                }

                _frame = -1;
            }
        }

        private static void SetDigit(Digit[] digitPool, DigitViewModel model)
        {
            var digit = digitPool[model.Index];
            if (model.IsAvoid)
            {
                digit.SetAvoid();
            }
            else
            {
                digit.SetDigit(model.Digit, model.DigitType);
            }
        }
    }
}
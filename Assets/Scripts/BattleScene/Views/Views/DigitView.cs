using System.Linq;
using BattleScene.Views.GameObjects;
using BattleScene.Views.ViewModels;
using Common;
using UnityEngine;

namespace BattleScene.Views.Views
{
    public class DigitView : MonoBehaviour
    {
        public float digitsIntervalSecond;

        // TODO: 以下のフィールドは子コンポーネントとして分離する。
        // [SerializeField] private float randomRange = 80.0f;
        // [SerializeField] private bool isPlayer;

        private Digit _digit;
        private readonly Digit[] _digitPool = new Digit[Constant.MaxAttackCount];
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

        private void Reset()
        {
            digitsIntervalSecond = 0.1f;
        }

        public void StartAnimation(DigitListViewModel model)
        {
            if (model.DigitList.Count == 0) return;

            foreach (var digit in _digitPool)
            {
                digit.ResetDigit();
            }

            model.DigitList
                .Aggregate(_digitPool, static (digitPool, digit) => 
                {
                    SetDigit(digitPool, digit);
                    return digitPool;
                });

            _animation = true;
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

        private void Update()
        {
            if (!_animation) return;

            ++_frame;
            if (_frame + 1 > _frameRate * digitsIntervalSecond)
            {
                ++_index;
                _digitPool[_index].enabled = true;

                // TODO: 以下の処理は子コンポーネントとして分離する。
                // var rand = isPlayer ? Mathf.Floor(Random.Range(-randomRange, randomRange) / 10) * 10.0f : 0;
                // _digitPool[_index].SetPosition(new Vector3(rand, rand, 0));

                if (_index + 1 >= _digitPool.Length)
                {
                    _index = -1;
                    _animation = false;
                }

                _frame = -1;
            }
        }
    }
}
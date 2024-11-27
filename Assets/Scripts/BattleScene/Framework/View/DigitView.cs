using System.Linq;
using System.Threading.Tasks;
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

        // ReSharper disable once RedundantDefaultMemberInitializer
        [SerializeField] private float alphaRate = 0.0f;
        [SerializeField] private int waitTime = 70;
        [SerializeField] private string avoidText = "avoid";

        [SerializeField] private bool isPlayer;
        private Digit _digit;
        private readonly Digit[] _digitPool = new Digit[PoolSize];

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
        }

        public async Task StartAnimationAsync(DigitListViewModel digitList)
        {
            for (var i = 0; i < PoolSize; ++i)
            {
                if (digitList.DigitList.Any(x => x.Index == i))
                {
                    var digit = digitList.DigitList.First(x => x.Index == i);
                    StartDigitAnimation(digit);
                }

                await Task.Delay(waitTime);
            }
        }

        private void StartDigitAnimation(DigitViewModel model)
        {
            var digit = _digitPool.First(x => !x.enabled);

            if (model.IsAvoid)
            {
                digit.SetAvoid();
            }
            else
            {
                digit.SetDigit(model.Digit, model.DigitType);
            }

            digit.enabled = true;
        }
    }
}
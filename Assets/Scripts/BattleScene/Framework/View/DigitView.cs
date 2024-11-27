using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.ViewModel;
using TMPro;
using UnityEngine;
using static BattleScene.Framework.Constant;
using Random = UnityEngine.Random;

namespace BattleScene.Framework.View
{
    public class DigitView : MonoBehaviour
    {
        [SerializeField] private int frame = 20;
        [SerializeField] private float moveRange = 20.0f;
        [SerializeField] private float randomRange = 80.0f;

        // ReSharper disable once RedundantDefaultMemberInitializer
        [SerializeField] private float alphaRate = 0.0f;
        [SerializeField] private int waitTime = 70;
        [SerializeField] private string avoidText = "avoid";

        private const int PoolSize = 10;
        // [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private bool isPlayer;
        private Digit _digit;
        private readonly Digit[] _digitPool = new Digit[10];

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
            var animationList = new List<Task>();
            for (var i = 0; i < PoolSize; ++i)
            {
                if (digitList.DigitList.Any(x => x.Index == i))
                {
                    var digit = digitList.DigitList.First(x => x.Index == i);
                    animationList.Add(StartDigitAnimationAsync(digit));
                }

                await Task.Delay(waitTime);
            }

            await Task.WhenAll(animationList);
        }

        private async Task StartDigitAnimationAsync(DigitViewModel model)
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
            var rand = isPlayer ? Mathf.Floor(Random.Range(-randomRange, randomRange) / 10) * 10.0f : 0;
            for (var i = 0; i < frame; ++i)
            {
                var sin = Mathf.Sin(i / (float)frame * 90 * Mathf.Deg2Rad);
                digit.transform.localPosition = new Vector3(rand, moveRange * sin + rand, 0);

                // var a = 1.0f - alphaRate * sin;
                // a = Mathf.Max(a, 0.0f);
                // digit.color = new Color(digit.color.r, digit.color.g, digit.color.b, a);

                await Task.Delay(WaitTime);
            }

            digit.enabled = false;
        }
    }
}
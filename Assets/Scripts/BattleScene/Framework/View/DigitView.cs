using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [SerializeField] private VertexGradient damageColor = new(Color.yellow, Color.yellow, Color.red, Color.red);
        [SerializeField] private VertexGradient cureColor = new(Color.green, Color.green, Color.cyan, Color.cyan);
        [SerializeField] private VertexGradient restoreColor = new(Color.cyan, Color.cyan, Color.blue, Color.blue);
        private const int PoolSize = 10;
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private bool isPlayer;
        private readonly List<TextMeshProUGUI> _textPool = new();

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

        private async Task StartDigitAnimationAsync(DigitViewModel digit)
        {
            if (_textPool.All(x => x.enabled))
            {
                // TODO: UpdateでInstantiateしている
                var tmpText = Instantiate(damageText, transform);
                tmpText.enabled = false;
                _textPool.Add(tmpText);
            }

            var text = _textPool.First(x => !x.enabled);

            if (digit.IsAvoid)
            {
                text.text = avoidText;
                text.enableVertexGradient = false;
            }
            else
            {
                text.text = digit.Digit.ToString();
                text.enableVertexGradient = true;
                text.colorGradient = digit.DigitType switch
                {
                    DigitType.Damage => damageColor,
                    DigitType.Cure => cureColor,
                    DigitType.Restore => restoreColor,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            text.enabled = true;
            var rand = isPlayer ? Mathf.Floor(Random.Range(-randomRange, randomRange) / 10) * 10.0f : 0;
            for (var i = 0; i < frame; ++i)
            {
                var sin = Mathf.Sin(i / (float)frame * 90 * Mathf.Deg2Rad);
                text.transform.localPosition = new Vector3(rand, moveRange * sin + rand, 0);

                var a = 1.0f - alphaRate * sin;
                a = Mathf.Max(a, 0.0f);
                text.color = new Color(text.color.r, text.color.g, text.color.b, a);

                await Task.Delay(WaitTime);
            }

            text.enabled = false;
        }
    }
}
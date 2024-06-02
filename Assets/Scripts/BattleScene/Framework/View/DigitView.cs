using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using TMPro;
using UnityEngine;
using static BattleScene.UserInterface.Constant;
using Random = UnityEngine.Random;

namespace BattleScene.UserInterface.View
{
    public class DigitView : MonoBehaviour, IDigitView
    {
        private const int Frame = 20;
        private const float MoveRange = 20.0f;
        private const float AlphaRate = 0.0f;
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private bool isPlayer;
        private readonly List<TextMeshProUGUI> _textPool = new();

        public async Task StartAnimation(IList<DigitDto> dtoList)
        {
            for (var i = 0; i < 10; ++i)
            {
                if (dtoList.Any(x => x.Index == i))
                {
                    var digitDto = dtoList.First(x => x.Index == i);
                    ViewDigit(digitDto);
                }

                await Task.Delay(70);
            }
        }

        private async void ViewDigit(DigitDto dto)
        {
            if (_textPool.All(x => x.enabled))
            {
                var tmpText = Instantiate(damageText, transform);
                tmpText.enabled = false;
                _textPool.Add(tmpText);
            }

            var text = _textPool.First(x => !x.enabled);

            if (dto.IsAvoid)
            {
                text.text = "avoid";
                text.enableVertexGradient = false;
            }
            else
            {
                text.text = dto.Digit.ToString();
                text.enableVertexGradient = true;
                text.colorGradient = dto.DigitColor switch
                {
                    DigitColor.Green => new VertexGradient(Color.green, Color.green, Color.cyan, Color.cyan),
                    DigitColor.Blue => new VertexGradient(Color.cyan, Color.cyan, Color.blue, Color.blue),
                    _ => new VertexGradient(Color.yellow, Color.yellow, Color.red, Color.red)
                };
            }

            text.enabled = true;
            var rand = isPlayer ? Mathf.Floor(Random.Range(-80.0f, 80.0f) / 10) * 10.0f : 0;
            for (var frame = 0; frame < Frame; ++frame)
            {
                var sin = Mathf.Sin(frame / (float)Frame * 90 * Mathf.Deg2Rad);
                text.transform.localPosition = new Vector3(rand, MoveRange * sin + rand, 0);

                var a = 1.0f - AlphaRate * sin;
                if (a <= 0.0f) a = 0.0f;
                text.color = new Color(text.color.r, text.color.g, text.color.b, a);

                await Task.Delay(WaitTime);
            }

            text.enabled = false;
        }
    }
}
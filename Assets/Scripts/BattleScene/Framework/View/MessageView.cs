using System.Threading;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using static BattleScene.Framework.Constant;

namespace BattleScene.Framework.View
{
    // 以下のページを参考にTextMeshProを使用するよう修正する
    // https://nekojara.city/unity-textmesh-pro-typewriter-effect
    // .maxVisibleCharactersプロパティを使用してゼロアロケーションの文字表示を実現すること
    public class MessageView : MonoBehaviour
    {
        private CancellationTokenSource _cancellationTokenSource;
        private Text _text;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _text = GetComponentInChildren<Text>();
            var window = GetComponentInChildren<Window>();
            window.Show();
        }

        public async Task StartAnimationAsync(MessageViewDto dto)
        {
            if (string.IsNullOrEmpty(dto.Message)) return;
            _text.enabled = true;
            if (dto.NoWait)
            {
                _text.text = dto.Message;
                return;
            }

            for (var i = 1; i < dto.Message.Length + 1; ++i)
            {
                _text.text = dto.Message.Substring(0, i);
                try
                {
                    await Task.Delay(WaitTime, _cancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                }
            }
        }

        public void StopAnimation()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            _text.text = "";
            _text.enabled = false;
        }
    }
}
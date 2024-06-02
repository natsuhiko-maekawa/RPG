using System.Threading;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.MessageView;
using UnityEngine;
using UnityEngine.UI;
using static BattleScene.Framework.Constant;

namespace BattleScene.Framework.View
{
    public class MessageView : MonoBehaviour, IMessageView
    {
        [SerializeField] private Text messageBoxText;
        [SerializeField] private Image window;
        private CancellationTokenSource _cancellationTokenSource;
        private Text _text;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Instantiate(window, transform);
            _text = Instantiate(messageBoxText, transform);
        }

        public async Task StartAnimation(MessageViewDto dto)
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
                await Task.Delay(WaitTime, _cancellationTokenSource.Token);
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
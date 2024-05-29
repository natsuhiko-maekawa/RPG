using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.PlayerView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using static BattleScene.UserInterface.Constant;

namespace BattleScene.UserInterface.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        private const int Frame = 10;
        private const float MoveRange = -20.0f;
        private DigitView _playerDigitView;
        private FrameView _playerFrameView;
        private StatusBarView _playerHpBarView;
        private StatusBarView _playerTpBarView;
        private PlayerVibesView _playerVibesView;
        private Image _image;
        private ISpriteFlyweight _spriteFlyweight;

        [Inject]
        public void Construct(
            ISpriteFlyweight spriteFlyweight)
        {
            _spriteFlyweight = spriteFlyweight;
        }
        
        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
            _playerDigitView = GetComponentInChildren<DigitView>();
            _playerFrameView = GetComponentInChildren<FrameView>();
            var playerHpBarView = GameObject.Find("PlayerHpBarView").transform;
            _playerHpBarView = playerHpBarView.GetComponent<StatusBarView>();
            var playerTpBarView = GameObject.Find("PlayerTpBarView").transform;
            _playerTpBarView = playerTpBarView.GetComponent<StatusBarView>();
            _playerVibesView = GetComponentInChildren<PlayerVibesView>();
        }

        public async Task StartAnimation(PlayerViewDto dto)
        {
            _image.sprite = _spriteFlyweight.Get(dto.PlayerImage);
            var originalPosition = new Vector3(0 - MoveRange, 0);

            for (var frame = 0; frame < Frame; ++frame)
            {
                var sin = Mathf.Sin(frame / (float)Frame * 90 * Mathf.Deg2Rad);
                _image.transform.localPosition = originalPosition + new Vector3(MoveRange * sin, 0, 0);
                await Task.Delay(WaitTime);
            }
        }

        public async Task StartPlayerDigitView(PlayerDigitViewDto dto)
        {
            await _playerDigitView.StartAnimation(dto.DigitDtoList);
        }

        public Task StartPlayerFrameView(PlayerFrameViewDto dto)
        {
            _playerFrameView.StartAnimation(dto.Color);
            return Task.CompletedTask;
        }

        public void StopPlayerFrameView()
        {
            _playerFrameView.StopAnimation();
        }
        
        public Task StartPlayerHpBarView(PlayerHpBarViewDto dto)
        {
            _playerHpBarView.StartAnimation(dto.StatusBarViewDto);
            return Task.CompletedTask;
        }
        
        public Task StartPlayerTpBarView(PlayerTpBarViewDto dto)
        {
            _playerTpBarView.StartAnimation(dto.StatusBarViewDto);
            return Task.CompletedTask;
        }
        
        public async Task StartPlayerVibesView()
        {
            await _playerVibesView.StartAnimation();
        }
    }
}
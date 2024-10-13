using System;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using static BattleScene.Framework.Constant;

namespace BattleScene.Framework.View
{
    public class PlayerView : MonoBehaviour
    {
        private const int Frame = 10;
        private const float MoveRange = -20.0f;
        private PlayerImage _playerImage;
        private DigitView _playerDigitView;
        private FrameView _playerFrameView;
        private StatusBarView _playerHpBarView;
        private StatusBarView _playerTpBarView;
        private PlayerVibesView _playerVibesView;
        private SpriteFlyweight _spriteFlyweight;

        private void Awake()
        {
            _playerImage = GetComponentInChildren<PlayerImage>();
            _playerDigitView = GetComponentInChildren<DigitView>();
            _playerFrameView = GetComponentInChildren<FrameView>();
            var statusBarViews = GetComponentsInChildren<StatusBarView>();
            _playerHpBarView = statusBarViews[0];
            _playerTpBarView = statusBarViews[1];
            _playerVibesView = GetComponentInChildren<PlayerVibesView>();
            _spriteFlyweight = SpriteFlyweight.Instance;
        }

        public async Task StartAnimation(PlayerViewDto dto)
        {
            try
            {
                var sprite = await _spriteFlyweight.GetAsync(dto.PlayerImage);
                _playerImage.Set(sprite);
                _playerImage.Show();
            }
            catch (ArgumentException)
            {
                _playerImage.Hide();
                _playerImage.SetText("NoImage");
                _playerImage.ShowText();
            }
            
            var originalPosition = new Vector3(0 - MoveRange, 0);

            for (var frame = 0; frame < Frame; ++frame)
            {
                var sin = Mathf.Sin(frame / (float)Frame * 90 * Mathf.Deg2Rad);
                _playerImage.transform.localPosition = originalPosition + new Vector3(MoveRange * sin, 0, 0);
                await Task.Delay(WaitTime);
            }
        }

        public async Task StartPlayerDigitView(DigitListViewModel model)
        {
            await _playerDigitView.StartAnimationAsync(model);
        }

        public Task StartFrameView(FrameViewDto dto)
        {
            _playerFrameView.StartAnimation(dto);
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

        public Task StartTechnicalPointBarView(TechnicalPointBarViewDto dto)
        {
            _playerTpBarView.StartAnimation(dto);
            return Task.CompletedTask;
        }

        public async Task StartPlayerVibesView()
        {
            await _playerVibesView.StartAnimation();
        }
    }
}
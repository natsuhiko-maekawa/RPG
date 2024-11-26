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
        private PlayerVibeView _playerVibeView;
        private SpriteFlyweight _spriteFlyweight;

        private void Awake()
        {
            _playerImage = GetComponentInChildren<PlayerImage>();
            _playerDigitView = GetComponentInChildren<DigitView>();
            _playerFrameView = GetComponentInChildren<FrameView>();
            var statusBarViews = GetComponentsInChildren<StatusBarView>();
            _playerHpBarView = statusBarViews[0];
            _playerTpBarView = statusBarViews[1];
            _playerVibeView = GetComponentInChildren<PlayerVibeView>();
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

            _playerImage.Slide();
        }

        public async Task StartPlayerDigitView(DigitListViewModel model)
        {
            await _playerDigitView.StartAnimationAsync(model);
        }

        public void StartFrameView(FrameViewDto dto)
        {
            _playerFrameView.StartAnimation(dto);
        }

        public void StopPlayerFrameView()
        {
            _playerFrameView.StopAnimation();
        }

        public void StartPlayerHpBarView(PlayerHpBarViewDto dto)
        {
            _playerHpBarView.StartAnimation(dto.StatusBarViewDto);
        }

        public Task StartPlayerTpBarView(PlayerTpBarViewDto dto)
        {
            _playerTpBarView.StartAnimation(dto.StatusBarViewDto);
            return Task.CompletedTask;
        }

        public void StartTechnicalPointBarView(TechnicalPointBarViewDto dto)
        {
            _playerTpBarView.StartAnimation(dto);
        }

        public void StartPlayerVibeView()
        {
            _playerImage.Vibe();
        }
    }
}
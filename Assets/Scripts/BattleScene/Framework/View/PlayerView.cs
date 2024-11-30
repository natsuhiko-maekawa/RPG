using System;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.ViewModel;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerImage _playerImage;
        private DigitView _playerDigitView;
        private FrameView _playerFrameView;
        private StatusBarView _playerHpBarView;
        private StatusBarView _playerTpBarView;
        private SpriteFlyweight _spriteFlyweight;

        private void Awake()
        {
            _playerImage = GetComponentInChildren<PlayerImage>();
            _playerImage.enabled = false;
            _playerDigitView = GetComponentInChildren<DigitView>();
            _playerFrameView = GetComponentInChildren<FrameView>();
            var statusBarViews = GetComponentsInChildren<StatusBarView>();
            _playerHpBarView = statusBarViews[0];
            _playerTpBarView = statusBarViews[1];
            _spriteFlyweight = SpriteFlyweight.Instance;
        }

        public async Task StartAnimation(PlayerViewModel model)
        {
            _playerImage.enabled = false;
            try
            {
                var sprite = await _spriteFlyweight.GetAsync(model.PlayerImage);
                _playerImage.Set(sprite);
            }
            catch (ArgumentException)
            {
                _playerImage.IsNothing();
            }

            _playerImage.enabled = true;
        }

        public void StartPlayerSlideView() => _playerImage.Slide();

        public void StartPlayerDigitView(DigitListViewModel model)
        {
            _playerDigitView.StartAnimation(model);
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.ViewModels;
using UnityEngine;
using Utility;

namespace BattleScene.Framework.Views
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerImage _playerImage;
        private DigitView _playerDigitView;
        private FrameView _playerFrameView;
        private StatusBarView _playerHpBarView;
        private StatusBarView _playerTpBarView;
        private SpriteFlyweight _spriteFlyweight;
        private Dictionary<string, Sprite> _imagePool;

        private void Awake()
        {
            _playerImage = GetComponentInChildren<PlayerImage>();
            _playerImage.enabled = false;
            _playerDigitView = GetComponentInChildren<DigitView>();
            _playerFrameView = GetComponentInChildren<FrameView>();
            var statusBarViews = GetComponentsInChildren<StatusBarView>();
            _playerHpBarView = statusBarViews[0];
            _playerTpBarView = statusBarViews[1];
        }

        public async Task SetImage(IReadOnlyList<string> playerImagePathList)
        {
            _spriteFlyweight = SpriteFlyweight.Instance;
            var capacity = playerImagePathList.Count;
            _imagePool = new Dictionary<string, Sprite>(capacity);
            foreach (var playerImagePath in playerImagePathList)
            {
                if (_imagePool.ContainsKey(playerImagePath)) return;

                try
                {
                    var sprite = await _spriteFlyweight.GetAsync(playerImagePath);
                    _imagePool.Add(playerImagePath, sprite);
                }
                catch (ArgumentException)
                {
                    MyDebug.LogWarning($"Image {playerImagePath} is not found.");
                }
            }
        }

        public void StartAnimation(PlayerViewModel model)
        {
            StopAnimation();
            if (_imagePool.TryGetValue(model.PlayerImagePath, out var sprite))
            {
                _playerImage.Set(sprite);
            }
            else
            {
                _playerImage.IsNothing();
            }

            _playerImage.enabled = true;
        }

        public void StopAnimation()
        {
            _playerImage.enabled = false;
        }

        public void StartPlayerSlideView() => _playerImage.Slide();

        public void StartPlayerDigitView(DigitListViewModel model)
        {
            _playerDigitView.StartAnimation(model);
        }

        public void StartFrameView(FrameViewModel model)
        {
            _playerFrameView.StartAnimation(model);
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
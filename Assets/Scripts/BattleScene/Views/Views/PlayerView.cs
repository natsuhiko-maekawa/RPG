using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Views.GameObjects;
using BattleScene.Views.ViewModels;
using UnityEngine;
using Utility;

namespace BattleScene.Views.Views
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
                if (_imagePool.ContainsKey(playerImagePath)) continue;

                try
                {
                    var sprite = await _spriteFlyweight.GetAsync(playerImagePath);
                    _imagePool.Add(playerImagePath, sprite);
                    MyDebug.Log(playerImagePath);
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
                MyDebug.Log(model.PlayerImagePath);
                _playerImage.IsNothing();
            }

            _playerImage.enabled = true;
        }

        public void StopAnimation()
        {
            _playerImage.enabled = false;
        }

        public void StartSlideAnimation() => _playerImage.Slide();
        public void StartDigitAnimation(DigitListViewModel model) => _playerDigitView.StartAnimation(model);
        public void StartFrameAnimation(FrameViewModel model) => _playerFrameView.StartAnimation(model);
        public void StopFrameAnimation() => _playerFrameView.StopAnimation();
        public void StartHitPointBarAnimation(StatusBarViewModel model) => _playerHpBarView.StartAnimation(model);
        public void StartTechnicalPointBarAnimation(StatusBarViewModel model) => _playerTpBarView.StartAnimation(model);
        public void StartVibeAnimation() => _playerImage.Vibe();
    }
}
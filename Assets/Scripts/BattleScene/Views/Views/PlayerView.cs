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
        private DigitView _digitView;
        private FrameView _frameView;
        private StatusBarView _hitPointBarView;
        private StatusBarView _technicalPointBarView;
        private Dictionary<string, Sprite> _imagePool;

        private void Awake()
        {
            _playerImage = GetComponentInChildren<PlayerImage>();
            _playerImage.enabled = false;
            _digitView = GetComponentInChildren<DigitView>();
            _frameView = GetComponentInChildren<FrameView>();
            _hitPointBarView = GetComponentInChildren<HitPointBarView>();
            _technicalPointBarView = GetComponentInChildren<TechnicalPointBarView>();
        }

        public async Task SetImage(IReadOnlyList<string> playerImagePathList)
        {
            var spriteFlyweight = SpriteFlyweight.Instance;
            var capacity = playerImagePathList.Count;
            _imagePool = new Dictionary<string, Sprite>(capacity);
            foreach (var playerImagePath in playerImagePathList)
            {
                if (_imagePool.ContainsKey(playerImagePath)) continue;

                try
                {
                    var sprite = await spriteFlyweight.GetAsync(playerImagePath);
                    _imagePool.Add(playerImagePath, sprite);
                }
                catch (ArgumentException)
                {
                    MyDebug.LogWarning("Image is not found.");
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

        public void StartSlideAnimation() => _playerImage.Slide();
        public void StartDigitAnimation(DigitListViewModel model) => _digitView.StartAnimation(model);
        public void StartFrameAnimation(FrameViewModel model) => _frameView.StartAnimation(model);
        public void StopFrameAnimation() => _frameView.StopAnimation();
        public void StartHitPointBarAnimation(StatusBarViewModel model) => _hitPointBarView.StartAnimation(model);
        public void StartTechnicalPointBarAnimation(StatusBarViewModel model) => _technicalPointBarView.StartAnimation(model);
        public void StartVibeAnimation() => _playerImage.Vibe();
    }
}
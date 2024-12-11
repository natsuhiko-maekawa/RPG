using System;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.ViewModel;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyImage _enemyImage;
        private EnemyAilmentView _enemyAilmentView;
        private DigitView _digitView;
        private FrameView _frameView;
        private StatusBarView _hitPointBarView;
        private SpriteFlyweight _spriteFlyweight;

        private void Awake()
        {
            _enemyImage = GetComponentInChildren<EnemyImage>();
            _enemyAilmentView = GetComponentInChildren<EnemyAilmentView>();
            _digitView = GetComponentInChildren<DigitView>();
            _frameView = GetComponentInChildren<FrameView>();
            _hitPointBarView = GetComponentInChildren<StatusBarView>();
            _spriteFlyweight = SpriteFlyweight.Instance;
        }

        private void OnEnable()
        {
            _enemyImage.enabled = true;
            _enemyAilmentView.enabled = true;
        }

        public async Task SetImage(string enemyImagePath)
        {
            var sprite = await _spriteFlyweight.GetAsync(enemyImagePath);
            _enemyImage.Set(sprite);
        }

        public void StartAilmentAnimationAsync(AilmentViewModel dto)
        {
            _enemyAilmentView.StartAnimation(dto);
        }

        public void StartDigitAnimation(DigitListViewModel model)
        {
            _digitView.StartAnimation(model);
        }

        public void StartHitPointBarAnimationAsync(EnemyHpBarViewDto dto)
        {
            _hitPointBarView.StartAnimation(dto.StatusBarViewDto);
        }

        public void StartFrameAnimationAsync(FrameViewDto dto)
        {
            _frameView.StartAnimation(dto);
        }

        public void StopFrameAnimation()
        {
            _frameView.StopAnimation();
        }

        public void StartVibeAnimation()
        {
            _enemyImage.Vibe();
        }

        private void OnDisable()
        {
            _enemyImage.enabled = false;
            _enemyAilmentView.enabled = false;
        }
    }
}
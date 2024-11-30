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

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public async Task SetImage(EnemyViewDto dto)
        {
            var enemyImagePath = dto.EnemyImagePath;
            var sprite = await _spriteFlyweight.GetAsync(enemyImagePath);
            _enemyImage.Set(sprite);
            _enemyImage.enabled = true;
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
    }
}
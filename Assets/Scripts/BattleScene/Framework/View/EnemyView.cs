using System.Threading.Tasks;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class EnemyView : MonoBehaviour
    {
        public Image Image { get;  set; }
        private EnemyAilmentView _enemyAilmentView;
        private DigitView _digitView;
        private FrameView _frameView;
        private StatusBarView _hitPointBarView;
        private EnemyVibesView _enemyVibesView;
        private SpriteFlyweight _spriteFlyweight;

        private void Awake()
        {
            Image = GetComponent<Image>();
            _enemyAilmentView = GetComponentInChildren<EnemyAilmentView>();
            _digitView = GetComponentInChildren<DigitView>();
            _frameView = GetComponentInChildren<FrameView>();
            _hitPointBarView = GetComponentInChildren<StatusBarView>();
            _enemyVibesView = GetComponentInChildren<EnemyVibesView>();
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
            Image.sprite = sprite;
        }

        public Task StartAilmentAnimationAsync(AilmentViewModel dto)
        {
            _enemyAilmentView.StartAnimation(dto);
            return Task.CompletedTask;
        }

        public async Task StartDigitAnimationAsync(DigitListViewModel model)
        {
            await _digitView.StartAnimationAsync(model);
        }

        public Task StartHitPointBarAnimationAsync(EnemyHpBarViewDto dto)
        {
            _hitPointBarView.StartAnimation(dto.StatusBarViewDto);
            return Task.CompletedTask;
        }

        public Task StartFrameAnimationAsync(FrameViewDto dto)
        {
            _frameView.StartAnimation(dto);
            return Task.CompletedTask;
        }

        public void StopFrameAnimation()
        {
            _frameView.StopAnimation();
        }

        public async Task StartVibesAnimationAsync()
        {
            await _enemyVibesView.StartAnimationAsync();
        }
    }
}
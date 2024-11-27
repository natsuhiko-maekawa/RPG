using System.Threading.Tasks;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class EnemyView : MonoBehaviour
    {
        public Image Image { get; set; }
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

        public async Task StartVibesAnimationAsync()
        {
            await _enemyVibesView.StartAnimationAsync();
        }
    }
}
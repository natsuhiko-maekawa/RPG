using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        private EnemyAilmentsView _enemyAilmentView;
        private DigitView _digitView;
        private FrameView _frameView;
        private StatusBarView _hitPointBarView;
        private EnemyVibesView _enemyVibesView;

        private void Awake()
        {
            _enemyAilmentView = GetComponentInChildren<EnemyAilmentsView>();
            _digitView = GetComponentInChildren<DigitView>();
            _frameView = GetComponentInChildren<FrameView>();
            _hitPointBarView = GetComponentInChildren<StatusBarView>();
            _enemyVibesView = GetComponentInChildren<EnemyVibesView>();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
        
        public Task StartAilmentAnimationAsync(EnemyAilmentsViewDto dto)
        {
            _enemyAilmentView.StartAnimation(dto);
            return Task.CompletedTask;
        }

        public async Task StartDigitAnimationAsync(EnemyDigitViewDto dto)
        {
            await _digitView.StartAnimation(dto.DigitDtoList);
        }

        public Task StartHitPointBarAnimationAsync(EnemyHpBarViewDto dto)
        {
            _hitPointBarView.StartAnimation(dto.StatusBarViewDto);
            return Task.CompletedTask;
        }

        public Task StartFrameAnimationAsync(FrameViewDto dto)
        {
            _frameView.StartAnimation(dto.Color);
            return Task.CompletedTask;
        }

        public void StopFrameAnimation()
        {
            _frameView.StopAnimation();
        }

        public async Task StartVibesAnimationAsync(EnemyVibesViewDto dto)
        {
            await _enemyVibesView.StartAnimation();
        }
    }
}
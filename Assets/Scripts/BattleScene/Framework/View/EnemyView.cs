using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace BattleScene.Framework.View
{
    public class EnemyView : IEnemyView
    {
        private readonly EnemyAilmentsView _ailmentsView;
        private readonly DigitView _dmgView;
        private readonly FrameView _frameView;
        private readonly StatusBarView _hpBarView;
        private readonly Image _image;
        private readonly EnemyVibesView _vibesView;
        
        public EnemyView(
            GameObject instance,
            Sprite sprite,
            Vector3 vector3,
            Transform transform)
        {
            instance.transform.SetParent(transform, false);
            instance.transform.localPosition += vector3;
            _vibesView = instance.GetComponentInChildren<EnemyVibesView>(); 
            _image = instance.GetComponentInChildren<Image>();
            _image.sprite = sprite;
            _ailmentsView = instance.GetComponentInChildren<EnemyAilmentsView>(); 
            _dmgView = instance.GetComponentInChildren<DigitView>(); 
            var enemyHpBarViewInstance = instance.GetComponentInChildren<StatusBarView>();
            enemyHpBarViewInstance.transform.localPosition += new Vector3(0, 40);
            _hpBarView = enemyHpBarViewInstance;
            _frameView = instance.GetComponentInChildren<FrameView>(); 
        }
        
        public Task StartAilmentView(EnemyAilmentsViewDto dto)
        {
            _ailmentsView.StartAnimation(dto);
            return Task.CompletedTask;
        }

        public async Task StartDigitView(EnemyDigitViewDto dto)
        {
            await _dmgView.StartAnimation(dto.DigitDtoList);
        }

        public Task StartHitPointBarView(EnemyHpBarViewDto dto)
        {
            _hpBarView.StartAnimation(dto.StatusBarViewDto);
            return Task.CompletedTask;
        }

        public Task StartFrameView(FrameViewDto dto)
        {
            _frameView.StartAnimation(dto.Color);
            return Task.CompletedTask;
        }

        public void StopFrameView()
        {
            _frameView.StopAnimation();
        }

        public async Task StartVibesView(EnemyVibesViewDto dto)
        {
            await _vibesView.StartAnimation();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.EnemyView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using UnityEngine;
using VContainer;
using Image = UnityEngine.UI.Image;

namespace BattleScene.UserInterface.View
{
    public class EnemiesView : MonoBehaviour, IEnemiesView
    {
        private const int MaxEnemyNum = 4;
        [SerializeField] private GameObject enemyView;
        private readonly List<EnemyAilmentsView> _enemyAilmentsViewList = new();
        private readonly List<DigitView> _enemyDmgViewList = new();
        private readonly List<FrameView> _enemyFrameViewList = new();
        private readonly List<StatusBarView> _enemyHpBarViewList = new();
        private readonly List<Image> _enemyImageList = new();
        private readonly List<EnemyVibesView> _enemyVibesViewList = new();
        private ISpriteFlyweight _spriteFlyweight;

        private void Awake()
        {
            for (var i = 0; i < MaxEnemyNum; ++i)
            {
                var enemyViewInstance = Instantiate(enemyView, transform);
                enemyViewInstance.transform.SetParent(transform, false);
                enemyViewInstance.SetActive(false);
                _enemyVibesViewList.Add(enemyViewInstance.GetComponentInChildren<EnemyVibesView>());
                _enemyImageList.Add(enemyViewInstance.GetComponentInChildren<Image>());
                _enemyAilmentsViewList.Add(enemyViewInstance.GetComponentInChildren<EnemyAilmentsView>());
                _enemyDmgViewList.Add(enemyViewInstance.GetComponentInChildren<DigitView>());
                var enemyHpBarViewInstance = enemyViewInstance.GetComponentInChildren<StatusBarView>();
                enemyHpBarViewInstance.transform.localPosition += new Vector3(0, 40);
                _enemyHpBarViewList.Add(enemyHpBarViewInstance);
                _enemyFrameViewList.Add(enemyViewInstance.GetComponentInChildren<FrameView>());
            }
        }

        // public void InitializeEnemyView(InitializeViewDto dto)
        // {
        //     foreach (var (enemy, index) in dto.EnemyDtoList.Select((x, i) => (x, i)))
        //     {
        //         _enemyImageList[index].sprite = _spriteFlyweight.Get(enemy.Name);
        //         _enemyViewList[index].transform.localPosition
        //             = new Vector3(EnemyPositionX[dto.EnemyDtoList.Count - 1][index], 0.0f);
        //         _enemyViewList[index].name = enemy.Name;
        //         _enemyViewList[index].SetActive(true);
        //         _enemyVibesViewList[index].Initialize();
        //     }
        // }

        public Task StartEnemyAilmentsView(EnemyAilmentsViewDto dto)
        {
            _enemyAilmentsViewList[dto.EnemyInt].StartAnimation(dto.AilmentsDtoList);
            return Task.CompletedTask;
        }

        public async Task StartEnemyDigitView(EnemyDigitViewDto dto)
        {
            await _enemyDmgViewList[dto.EnemyInt].StartAnimation(dto.DigitDtoList);
        }

        public Task StartEnemyHpBarView(EnemyHpBarViewDto dto)
        {
            _enemyHpBarViewList[dto.EnemyInt].StartAnimation(dto.StatusBarViewDto);
            return Task.CompletedTask;
        }

        public Task StartEnemyFrameView(EnemyFrameViewDto dto)
        {
            _enemyFrameViewList[dto.EnemyInt].StartAnimation(dto.Color);
            return Task.CompletedTask;
        }

        public void StopEnemyFrameView()
        {
            foreach (var view in _enemyFrameViewList.Where(x => x.isActiveAndEnabled)) view.StopAnimation();
        }

        public async Task StartEnemyVibesView(EnemyVibesViewDto dto)
        {
            await _enemyVibesViewList[dto.EnemyInt].StartAnimation();
        }

        public Task StartEnemyView(List<EnemyViewDto> dtoList)
        {
            foreach (var (enemyImage, index) in _enemyImageList.Select((x, i) => (x, i)))
                enemyImage.enabled = dtoList.Exists(x => x.EnemyInt == index);

            return Task.CompletedTask;
        }
        // private static readonly float[][] EnemyPositionX =
        // {
        //     new[] { 0.0f },
        //     new[] { -100.0f, 100.0f },
        //     new[] { -200.0f, 0.0f, 200.0f },
        //     new[] { -300.0f, -100.0f, 100.0f, 300.0f }
        // };

        [Inject]
        public void Construct(
            ISpriteFlyweight spriteFlyweight)
        {
            _spriteFlyweight = spriteFlyweight;
        }
    }
}
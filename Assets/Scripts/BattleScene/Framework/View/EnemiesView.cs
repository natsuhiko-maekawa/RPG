using System.Collections.Generic;
using System.Collections.Immutable;
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

namespace BattleScene.Framework.View
{
    public class EnemiesView : MonoBehaviour, IEnemiesView
    {
        private const int MaxEnemyNum = 4;
        [SerializeField] private GameObject enemyView;
        private ImmutableList<IEnemyView> _enemyViewList;
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
            _enemyAilmentsViewList[dto.EnemyNumber].StartAnimation(dto);
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

        public IEnemyView this[int i] => _enemyViewList[i];

        public async Task StartEnemyView(EnemyViewDto dto)
        {
            var enemyViewArray = await Task.WhenAll(Enumerable.Range(0, dto.EnemyCount)
                .Where(x => dto.EnemyDtoList.Any(y => y.EnemyNumber == x))
                .Select(async x =>
                {
                    var instance = Instantiate(enemyView, transform);
                    var enemyImagePath = dto.EnemyDtoList.First(y => y.EnemyNumber == x).EnemyImagePath;
                    var sprite = await _spriteFlyweight.Get(enemyImagePath);
                    var vector3 = new Vector3(X[dto.EnemyCount - 1][x], 0.0f);
                    return new EnemyView(
                        instance: instance,
                        sprite: sprite,
                        vector3: vector3,
                        transform: transform);
                }));
            
            _enemyViewList = enemyViewArray.ToImmutableList<IEnemyView>();
        }
        
        private static readonly float[][] X =
        {
            new[] { 0.0f },
            new[] { -100.0f, 100.0f },
            new[] { -200.0f, 0.0f, 200.0f },
            new[] { -300.0f, -100.0f, 100.0f, 300.0f }
        };

        [Inject]
        public void Construct(
            ISpriteFlyweight spriteFlyweight)
        {
            _spriteFlyweight = spriteFlyweight;
        }
    }
}
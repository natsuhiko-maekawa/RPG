using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Views.Views;
using Common;
using Utility;

namespace BattleScene.Presenters.Presenters
{
    public class EnemyImageViewPresenter
    {
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly EnemyGroupView _enemyGroupView;
        [ForCache] private readonly List<EnemyViewDto> _dtoList = new(Constant.MaxEnemyCount);

        public EnemyImageViewPresenter(
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            EnemyGroupView enemyGroupView)
        {
            _enemyViewInfoResource = enemyViewInfoResource;
            _enemyGroupView = enemyGroupView;
        }

        public async Task SetImage(IReadOnlyList<CharacterEntity> characterList)
        {
            var taskArray = characterList
                .Where(x => !x.IsPlayer)
                .Join(
                    inner: GetEnemyViewResource(),
                    outerKeySelector: static characterEntity => characterEntity.CharacterTypeCode,
                    innerKeySelector: static dto => dto.Key,
                    resultSelector: static (_, inner) => inner.ImagePath)
                .Zip(_enemyGroupView, static (enemyImagePath, enemyView) => (enemyImagePath, enemyView))
                .Select(static x => x.enemyView.SetImage(x.enemyImagePath))
                .ToArray();
            await Task.WhenAll(taskArray);
        }

        public void StartAnimation(int enemyCount)
        {
            _enemyGroupView.StartAnimation(enemyCount);
        }

        public void StopAnimation(int position)
        {
            _enemyGroupView[position].enabled = false;
        }

        private List<EnemyViewDto> GetEnemyViewResource()
        {
            _enemyViewInfoResource.Get(_dtoList);
            return _dtoList;
        }
    }
}
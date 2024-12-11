using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Framework.View;
using Utility;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class EnemyImageViewPresenter
    {
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly EnemiesView _enemiesView;
        // TODO: キャパシティを設定する。
        [ForCache] private readonly List<EnemyViewDto> _dtoList = new();

        public EnemyImageViewPresenter(
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            EnemiesView enemiesView)
        {
            _enemyViewInfoResource = enemyViewInfoResource;
            _enemiesView = enemiesView;
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
                .Zip(_enemiesView, static (enemyImagePath, enemyView) => (enemyImagePath, enemyView))
                .Select(static x => x.enemyView.SetImage(x.enemyImagePath))
                .ToArray();
            await Task.WhenAll(taskArray);
        }

        private List<EnemyViewDto> GetEnemyViewResource()
        {
            _enemyViewInfoResource.Get(_dtoList);
            return _dtoList;
        }

        public void StartAnimation()
        {
            _enemiesView.enabled = true;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class EnemyImagePresenter
    {
        private readonly EnemiesDomainService _enemies;
        private readonly IResource<DataAccess.Dto.EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly EnemiesView _enemiesView;

        public EnemyImagePresenter(
            EnemiesDomainService enemies,
            IResource<DataAccess.Dto.EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            EnemiesView enemiesView)
        {
            _enemies = enemies;
            _enemyViewInfoResource = enemyViewInfoResource;
            _enemiesView = enemiesView;
        }

        public async void Show()
        {
            var taskList = _enemies.GetSurvive()
                .Select(GetDto)
                .Select(GetTask)
                .ToList();

            await Task.WhenAll(taskList);
        }

        private KeyValuePair<int, EnemyViewDto> GetDto(CharacterEntity characterEntity)
        {
            var characterTypeId = characterEntity.CharacterTypeCode;
            var enemyNumber = characterEntity.Position;
            var enemyImagePath = _enemyViewInfoResource.Get(characterTypeId).ImagePath;
            var dto = new EnemyViewDto(
                EnemyImagePath: enemyImagePath);
            var indexDtoPair = new KeyValuePair<int, EnemyViewDto>(enemyNumber, dto);
            return indexDtoPair;
        }

        private Task GetTask(KeyValuePair<int, EnemyViewDto> indexDtoPair)
        {
            _enemiesView[indexDtoPair.Key].SetActive(true);
            var task = _enemiesView[indexDtoPair.Key].SetImage(indexDtoPair.Value);
            return task;
        }
    }
}
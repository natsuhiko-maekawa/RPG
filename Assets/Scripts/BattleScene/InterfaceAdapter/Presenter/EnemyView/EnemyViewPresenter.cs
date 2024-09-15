using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.UseCases.View.EnemyView.OutputBoundary;
using BattleScene.UseCases.View.EnemyView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.EnemyView
{
    internal class EnemyViewPresenter : IEnemyViewPresenter
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;
        private readonly IResource<DataAccess.Dto.EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly IEnemiesView _enemiesView;

        public EnemyViewPresenter(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<EnemyEntity, CharacterId> enemyRepository,
            IResource<DataAccess.Dto.EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            IEnemiesView enemiesView)
        {
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
            _enemyViewInfoResource = enemyViewInfoResource;
            _enemiesView = enemiesView;
        }

        public void Start(EnemyOutputData enemyOutputData)
        {
            enemyOutputData.EnemyCharacterIdList
                .Select(GetImagePath)
                .ToImmutableList()
                .ForEach(StartEnemyView);
        }

        private KeyValuePair<int, string> GetImagePath(CharacterId characterId)
        {
            var characterTypeId = _characterRepository.Select(characterId).CharacterTypeCode; 
            var enemyNumber = _enemyRepository.Select(characterId).EnemyNumber;
            var enemyImagePath = _enemyViewInfoResource.Get(characterTypeId).EnemyImagePath;
            var indexImagePathPair = new KeyValuePair<int, string>(enemyNumber, enemyImagePath);
            return indexImagePathPair;
        }

        private void StartEnemyView(KeyValuePair<int, string> indexImagePathPair)
        {
            var dto = new EnemyViewDto(
                EnemyImagePath: indexImagePathPair.Value);
            _enemiesView[indexImagePathPair.Key].SetImage(dto);
        }
    }
}
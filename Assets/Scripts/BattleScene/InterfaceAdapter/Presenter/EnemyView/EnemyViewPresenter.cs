using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases.View.EnemyView.OutputBoundary;
using BattleScene.UseCases.View.EnemyView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.EnemyView
{
    internal class EnemyViewPresenter : IEnemyViewPresenter
    {
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;
        private readonly IEnemyViewInfoFactory _enemyViewInfoFactory;
        private readonly IEnemiesView _enemiesView;

        public EnemyViewPresenter(
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRepository<EnemyEntity, CharacterId> enemyRepository,
            IEnemyViewInfoFactory enemyViewInfoFactory,
            IEnemiesView enemiesView)
        {
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
            _enemyViewInfoFactory = enemyViewInfoFactory;
            _enemiesView = enemiesView;
        }

        public void Start(EnemyOutputData enemyOutputData)
        {
            var enemyDto = enemyOutputData.EnemyCharacterIdList
                .Select(x =>
                {
                    var characterTypeId = _characterRepository.Select(x).Property.CharacterTypeId;
                    return new EnemyDto(
                        EnemyNumber: _enemyRepository.Select(x).EnemyNumber,
                        EnemyImagePath: _enemyViewInfoFactory.Create(characterTypeId).EnemyImagePath);
                })
                .ToImmutableList();
            var enemyViewDto = new EnemyViewDto(
                EnemyCount: enemyOutputData.EnemyCharacterIdList.Count,
                EnemyDtoList: enemyDto);
            
            _enemiesView.StartEnemyView(enemyViewDto);
        }
    }
}
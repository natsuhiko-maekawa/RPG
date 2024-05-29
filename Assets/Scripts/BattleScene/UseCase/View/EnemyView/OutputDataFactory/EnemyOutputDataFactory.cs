using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.View.EnemyView.OutputData;

namespace BattleScene.UseCase.View.EnemyView.OutputDataFactory
{
    public class EnemyOutputDataFactory
    {
        private readonly IEnemyRepository _enemyRepository;
        private readonly IHitPointRepository _hitPointRepository;
        private readonly IEnemyViewInfoFactory _enemyViewInfoFactory;
        private readonly CharactersDomainService _characters;

        public EnemyOutputDataFactory(
            IEnemyRepository enemyRepository,
            IHitPointRepository hitPointRepository,
            IEnemyViewInfoFactory enemyViewInfoFactory,
            CharactersDomainService characters)
        {
            _enemyRepository = enemyRepository;
            _hitPointRepository = hitPointRepository;
            _enemyViewInfoFactory = enemyViewInfoFactory;
            _characters = characters;
        }

        public ImmutableList<EnemyOutputData> Create()
        {
            return _characters.GetEnemies()
                .Select(x => new EnemyOutputData(
                    EnemyNumber: _enemyRepository.Select(x.CharacterId).EnemyNumber,
                    EnemyImagePath: _enemyViewInfoFactory.Create(x.Property.CharacterTypeId).EnemyImagePath,
                    IsSurvive: _hitPointRepository.Select(x.CharacterId).IsSurvive()))
                .ToImmutableList();
        }
    }
}
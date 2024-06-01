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
        private readonly CharactersDomainService _characters;
        private readonly IEnemyRepository _enemyRepository;
        private readonly IEnemyViewInfoFactory _enemyViewInfoFactory;
        private readonly IHitPointRepository _hitPointRepository;

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

        public EnemyOutputDataFactory(
            CharactersDomainService characters,
            IEnemyRepository enemyRepository,
            IEnemyViewInfoFactory enemyViewInfoFactory,
            IHitPointRepository hitPointRepository)
        {
            _characters = characters;
            _enemyRepository = enemyRepository;
            _enemyViewInfoFactory = enemyViewInfoFactory;
            _hitPointRepository = hitPointRepository;
        }

        public ImmutableList<EnemyOutputData> Create()
        {
            return _characters.GetEnemies()
                .Select(x => new EnemyOutputData(
                    _enemyRepository.Select(x.CharacterId).EnemyNumber,
                    _enemyViewInfoFactory.Create(x.Property.CharacterTypeId).EnemyImagePath,
                    _hitPointRepository.Select(x.CharacterId).IsSurvive()))
                .ToImmutableList();
        }
    }
}
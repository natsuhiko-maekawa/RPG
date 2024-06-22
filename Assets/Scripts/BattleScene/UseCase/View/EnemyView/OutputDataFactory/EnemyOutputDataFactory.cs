using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
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
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;

        public EnemyOutputDataFactory(
            CharactersDomainService characters,
            IEnemyRepository enemyRepository,
            IEnemyViewInfoFactory enemyViewInfoFactory,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository)
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
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.CharacterVibesView.OutputData;

namespace BattleScene.UseCase.View.CharacterVibesView.OutputDataFactory
{
    public class CharacterVibesOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyRepository _enemyRepository;
        private readonly ResultDomainService _result;

        public CharacterVibesOutputDataFactory(
            ICharacterRepository characterRepository,
            IEnemyRepository enemyRepository,
            ResultDomainService result)
        {
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
            _result = result;
        }

        public ImmutableList<CharacterVibesOutputData> Create()
        {
            return _result.LastDamage().DamageList
                .GroupBy(x => x.TargetId)
                .Where(x => x
                    .Any(y => y.IsHit))
                .Select(x => new CharacterVibesOutputData(
                    _characterRepository.Select(x.Key).IsPlayer()
                        ? CharacterOutputData.SetPlayer()
                        : CharacterOutputData.SetEnemy(_enemyRepository.Select(x.Key).EnemyNumber)))
                .ToImmutableList();
        }
    }
}
namespace BattleScene.UseCases.View.HitPointBarView.OutputDataFactory
{
    public class HitPointBarOutputDataFactory
    {
        // private readonly ICharacterRepository _characterRepository;
        // private readonly IEnemyRepository _enemyRepository;
        // private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        // private readonly ResultDomainService _result;
        //
        // public HitPointBarOutputDataFactory(
        //     ICharacterRepository characterRepository,
        //     IEnemyRepository enemyRepository,
        //     IRepository<HitPointAggregate, CharacterId> hitPointRepository,
        //     ResultDomainService result)
        // {
        //     _characterRepository = characterRepository;
        //     _enemyRepository = enemyRepository;
        //     _hitPointRepository = hitPointRepository;
        //     _result = result;
        // }
        //
        // public ImmutableList<HitPointBarOutputData> Create()
        // {
        //     return _result.LastDamage().AttackList
        //         .GroupBy(x => x.TargetId)
        //         .Where(x => x.Any(y => y.IsHit))
        //         .Select(x => x
        //             .Select(y => (targetId: y.TargetId, digit: y.Amount))
        //             .Aggregate((y, z) => (y.targetId, y.digit + z.digit)))
        //         .Select(x => new HitPointBarOutputData(
        //             _characterRepository.Select(x.targetId).IsPlayer
        //                 ? CharacterOutputData.SetPlayer()
        //                 : CharacterOutputData.SetEnemy(_enemyRepository.Select(x.targetId).EnemyNumber),
        //             _hitPointRepository.Select(x.targetId).GetMax(),
        //             _hitPointRepository.Select(x.targetId).GetCurrent()))
        //         .ToImmutableList();
        // }
    }
}
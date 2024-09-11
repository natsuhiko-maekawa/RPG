namespace BattleScene.UseCases.View.CharacterVibesView.OutputDataFactory
{
    public class CharacterVibesOutputDataFactory
    {
        // public ImmutableList<CharacterVibesOutputData> Create()
        // {
        //     return _result.LastDamage().AttackList
        //         .GroupBy(x => x.TargetId)
        //         .Where(x => x
        //             .Any(y => y.IsHit))
        //         .Select(x => new CharacterVibesOutputData(
        //             _characterRepository.Select(x.Key).IsPlayer
        //                 ? CharacterOutputData.SetPlayer()
        //                 : CharacterOutputData.SetEnemy(_enemyRepository.Select(x.Key).EnemyNumber)))
        //         .ToImmutableList();
        // }
    }
}
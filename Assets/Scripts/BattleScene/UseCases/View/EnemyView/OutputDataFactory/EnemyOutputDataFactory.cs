using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.View.EnemyView.OutputData;

namespace BattleScene.UseCases.View.EnemyView.OutputDataFactory
{
    public class EnemyOutputDataFactory
    {
        private readonly CharactersDomainService _characters;

        public EnemyOutputDataFactory(
            CharactersDomainService characters)
        {
            _characters = characters;
        }

        public EnemyOutputData Create()
        {
            var enemyCharacterIdList = _characters.GetEnemies()
                .Select(x => x.CharacterId)
                .ToImmutableList();
            return new EnemyOutputData(enemyCharacterIdList);
        }
    }
}
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.IResource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class EnemyViewInfoFactory : IEnemyViewInfoFactory
    {
        private readonly IEnemyViewInfoResource _enemyViewInfoResource;

        public EnemyViewInfoFactory(
            IEnemyViewInfoResource enemyViewInfoResource)
        {
            _enemyViewInfoResource = enemyViewInfoResource;
        }

        public EnemyViewInfoValueObject Create(CharacterTypeId characterTypeId)
        {
            var enemyViewInfoDto = _enemyViewInfoResource.Get()
                .First(x => x.EnemyTypeId == characterTypeId);
            var enemyName = characterTypeId.ToString();
            return new EnemyViewInfoValueObject(
                characterTypeId: characterTypeId,
                enemyName: enemyViewInfoDto.enemyName,
                enemyImagePath: $"{enemyName}[{enemyName}]");
        }
    }
}
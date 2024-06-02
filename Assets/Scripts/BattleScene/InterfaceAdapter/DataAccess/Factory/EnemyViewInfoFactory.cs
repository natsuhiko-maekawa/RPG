using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class EnemyViewInfoFactory : IEnemyViewInfoFactory
    {
        public EnemyViewInfoValueObject Create(CharacterTypeId characterTypeId)
        {
            throw new System.NotImplementedException();
        }
    }
}
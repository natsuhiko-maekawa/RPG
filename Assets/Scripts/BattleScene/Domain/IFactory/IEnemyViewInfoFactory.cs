using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    public interface IEnemyViewInfoFactory
    {
        public EnemyViewInfoValueObject Create(CharacterTypeId characterTypeId);
    }
}
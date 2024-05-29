using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    public interface IPlayerPropertyFactory
    {
        public PlayerPropertyValueObject Get();
    }
}
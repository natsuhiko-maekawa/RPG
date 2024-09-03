using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DataAccess.ObsoleteIFactory
{
    public interface IPlayerPropertyFactory
    {
        public PlayerPropertyValueObject Get();
    }
}
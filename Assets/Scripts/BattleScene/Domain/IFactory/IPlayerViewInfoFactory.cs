using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    public interface IPlayerViewInfoFactory
    {
        public PlayerViewInfoValueObject Create();
    }
}
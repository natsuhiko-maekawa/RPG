using BattleScene.DataAccess.Dto;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;

namespace BattleScene.DataAccess.Factory
{
    public class BattlePropertyFactory : IFactory<BattlePropertyValueObject>
    {
        private readonly IResource<BattlePropertyDto> _battlePropertyResource;

        public BattlePropertyFactory(
            IResource<BattlePropertyDto> battlePropertyResource)
        {
            _battlePropertyResource = battlePropertyResource;
        }

        public BattlePropertyValueObject Create()
        {
            var battleProperty = _battlePropertyResource.Get();
            var battlePropertyValueObject = new BattlePropertyValueObject(
                SlipDefaultTurn: battleProperty.SlipDefaultTurn,
                SlipDefaultDamageRate: battleProperty.SlipDefaultDamageRate,
                IsHitThreshold: battleProperty.IsHitThreshold,
                MaxAgility: battleProperty.MaxAgility);
            return battlePropertyValueObject;
        }
    }
}
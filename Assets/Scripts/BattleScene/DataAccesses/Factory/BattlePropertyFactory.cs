using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.ValueObjects;

namespace BattleScene.DataAccesses.Factory
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
                AilmentSuccessThreshold: battleProperty.AilmentSuccessThreshold,
                MaxAgility: battleProperty.MaxAgility,
                MaxOrderCount: battleProperty.MaxOrderCount,
                AttackCountLimit: battleProperty.AttackCountLimit);
            return battlePropertyValueObject;
        }
    }
}
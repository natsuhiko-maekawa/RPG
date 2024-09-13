using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
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
                slipDefaultTurn: battleProperty.SlipDefaultTurn);
            return battlePropertyValueObject;
        }
    }
}
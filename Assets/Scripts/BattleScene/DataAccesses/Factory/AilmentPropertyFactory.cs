using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.ValueObjects;

namespace BattleScene.DataAccesses.Factory
{
    public class AilmentPropertyFactory : IFactory<AilmentPropertyValueObject, AilmentCode>
    {
        private readonly IResource<AilmentPropertyDto, AilmentCode> _ailmentPropertyResource;

        public AilmentPropertyFactory(
            IResource<AilmentPropertyDto, AilmentCode> ailmentPropertyResource)
        {
            _ailmentPropertyResource = ailmentPropertyResource;
        }

        public AilmentPropertyValueObject Create(AilmentCode key)
        {
            var dto = _ailmentPropertyResource.Get(key);
            var ailmentProperty = new AilmentPropertyValueObject(
                ailmentCode: dto.Key,
                defaultTurn: dto.DefaultTurn,
                isSelfRecovery: dto.IsSelfRecovery,
                priority: dto.Priority);
            return ailmentProperty;
        }
    }
}
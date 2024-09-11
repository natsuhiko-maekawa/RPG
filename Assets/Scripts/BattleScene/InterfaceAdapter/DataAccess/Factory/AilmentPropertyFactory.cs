using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
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
                turn: dto.Turn,
                isSelfRecovery: dto.IsSelfRecovery,
                priority: dto.Priority);
            return ailmentProperty;
        }
    }
}
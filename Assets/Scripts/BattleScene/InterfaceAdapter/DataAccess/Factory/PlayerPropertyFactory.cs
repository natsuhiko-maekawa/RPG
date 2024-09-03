using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class PlayerPropertyFactory : IFactory<PlayerPropertyValueObject, CharacterTypeCode>
    {
        private readonly IResource<PlayerPropertyDto, CharacterTypeCode> _resource;

        public PlayerPropertyFactory(
            IResource<PlayerPropertyDto, CharacterTypeCode> resource)
        {
            _resource = resource;
        }

        public PlayerPropertyValueObject Create(CharacterTypeCode key)
        {
            var dto = _resource.Get(key);
            return new PlayerPropertyValueObject(
                technicalPoint: dto.TechnicalPoint,
                fatalitySkills: dto.FatalitySkills);
        }
    }
}
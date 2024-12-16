using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.ValueObjects;

namespace BattleScene.DataAccesses.Factories
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
                fatalitySkillList: dto.FatalitySkillList);
        }
    }
}
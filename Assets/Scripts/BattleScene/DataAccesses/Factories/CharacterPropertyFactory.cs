using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.ValueObjects;

namespace BattleScene.DataAccesses.Factories
{
    public class CharacterPropertyFactory : IFactory<CharacterPropertyValueObject, CharacterTypeCode>
    {
        private readonly IResource<CharacterPropertyDto, CharacterTypeCode> _propertyResource;

        public CharacterPropertyFactory(
            IResource<CharacterPropertyDto, CharacterTypeCode> propertyResource)
        {
            _propertyResource = propertyResource;
        }

        public CharacterPropertyValueObject Create(CharacterTypeCode key)
        {
            var property = _propertyResource.Get(key);
            return new CharacterPropertyValueObject(
                characterTypeCode: property.Key,
                hitPoint: property.HitPoint,
                strength: property.Strength,
                vitality: property.Vitality,
                intelligence: property.Intelligence,
                wisdom: property.Wisdom,
                agility: property.Agility,
                luck: property.Luck,
                weakPointCode: property.WeakPointCode,
                skillCodeList: property.SkillCodeList);
        }
    }
}
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;

namespace BattleScene.DataAccess.Factory
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
                weakPointCodeList: property.WeakPointCodeList,
                skillCodeList: property.SkillCodeList);
        }
    }
}
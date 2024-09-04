using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class PropertyFactory : IFactory<PropertyValueObject, CharacterTypeCode>
    {
        private readonly IResource<PropertyDto, CharacterTypeCode> _propertyResource;

        public PropertyFactory(
            IResource<PropertyDto, CharacterTypeCode> propertyResource)
        {
            _propertyResource = propertyResource;
        }

        public PropertyValueObject Create(CharacterTypeCode key)
        {
            var property = _propertyResource.Get(key);
            return new PropertyValueObject(
                characterTypeCode: property.Key,
                hitPoint: property.HitPoint,
                strength: property.Strength,
                vitality: property.Vitality,
                intelligence: property.Intelligence,
                wisdom: property.Wisdom,
                agility:property.Agility,
                luck: property.Luck,
                weakPoints: property.WeakPoints, 
                skills: property.Skills);
        }
    }
}
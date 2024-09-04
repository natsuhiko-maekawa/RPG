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

        // [Obsolete]
        // public PropertyValueObject Get(CharacterTypeCode characterTypeCode)
        // {
        //     return _propertyResource.Get()
        //         .Where(x => x.Key == characterTypeCode)
        //         .Select(x => new PropertyValueObject(x.Key, x.HitPoint, x.Strength, x.Vitality, x.Intelligence,
        //             x.Wisdom, x.Agility, x.Luck, x.WeakPoints, x.Skills))
        //         .First();
        // }
        //
        // public ImmutableList<PropertyValueObject> Get(IList<CharacterTypeCode> characterTypeIdList)
        // {
        //     return characterTypeIdList.Select(Get).ToImmutableList();
        // }
        //
        // public ImmutableList<PropertyValueObject> Get()
        // {
        //     return _propertyResource.Get()
        //         .Select(x => new PropertyValueObject(x.Key, x.hp, x.strength, x.vitality, x.intelligence,
        //             x.wisdom, x.agility, x.luck, x.WeakPoints, x.Skills))
        //         .ToImmutableList();
        // }

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
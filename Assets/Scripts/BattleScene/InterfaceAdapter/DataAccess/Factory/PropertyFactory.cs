using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.IResource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class PropertyFactory : IPropertyFactory
    {
        private readonly IPropertyResource _propertyResource;

        public PropertyFactory(
            IPropertyResource propertyResource)
        {
            _propertyResource = propertyResource;
        }

        public PropertyValueObject Get(CharacterTypeCode characterTypeCode)
        {
            return _propertyResource.Get()
                .Where(x => x.CharacterTypeCode == characterTypeCode)
                .Select(x => new PropertyValueObject(x.CharacterTypeCode, x.hp, x.strength, x.vitality, x.intelligence,
                    x.wisdom, x.agility, x.luck, x.WeakPoints, x.Skills))
                .First();
        }

        public ImmutableList<PropertyValueObject> Get(IList<CharacterTypeCode> characterTypeIdList)
        {
            return characterTypeIdList.Select(Get).ToImmutableList();
        }

        public ImmutableList<PropertyValueObject> Get()
        {
            return _propertyResource.Get()
                .Select(x => new PropertyValueObject(x.CharacterTypeCode, x.hp, x.strength, x.vitality, x.intelligence,
                    x.wisdom, x.agility, x.luck, x.WeakPoints, x.Skills))
                .ToImmutableList();
        }
    }
}
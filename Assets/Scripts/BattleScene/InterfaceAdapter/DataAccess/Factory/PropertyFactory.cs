using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
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

        public PropertyValueObject Get(CharacterTypeId characterTypeId)
        {
            return _propertyResource.Get()
                .Where(x => x.CharacterTypeId == characterTypeId)
                .Select(x => new PropertyValueObject(x.CharacterTypeId, x.hp, x.strength, x.vitality, x.intelligence,
                    x.wisdom, x.agility, x.luck, x.WeakPoints, x.Skills))
                .First();
        }

        public ImmutableList<PropertyValueObject> Get(IList<CharacterTypeId> characterTypeIdList)
        {
            return characterTypeIdList.Select(Get).ToImmutableList();
        }

        public ImmutableList<PropertyValueObject> Get()
        {
            return _propertyResource.Get()
                .Select(x => new PropertyValueObject(x.CharacterTypeId, x.hp, x.strength, x.vitality, x.intelligence,
                    x.wisdom, x.agility, x.luck, x.WeakPoints, x.Skills))
                .ToImmutableList();
        }
    }
}
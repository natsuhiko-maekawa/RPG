using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    public interface IPropertyFactory
    {
        public PropertyValueObject Get(CharacterTypeId characterTypeId);
        public ImmutableList<PropertyValueObject> Get(IList<CharacterTypeId> characterTypeIdList);
        public ImmutableList<PropertyValueObject> GetAll();
    }
}
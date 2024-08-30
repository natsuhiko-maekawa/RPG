using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    public interface IPropertyFactory
    {
        public PropertyValueObject Get(CharacterTypeCode characterTypeCode);
        public ImmutableList<PropertyValueObject> Get(IList<CharacterTypeCode> characterTypeIdList);
        public ImmutableList<PropertyValueObject> Get();
    }
}
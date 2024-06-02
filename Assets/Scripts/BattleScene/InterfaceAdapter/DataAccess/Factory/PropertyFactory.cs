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
        private readonly IBattleSceneScriptableObject _battleSceneScriptableObject;

        public PropertyFactory(
            IBattleSceneScriptableObject battleSceneScriptableObject)
        {
            _battleSceneScriptableObject = battleSceneScriptableObject;
        }

        public PropertyValueObject Get(CharacterTypeId key)
        {
            return _battleSceneScriptableObject.GetPropertyScriptableObject()
                .Where(x => x.Key == key)
                .Select(x => new PropertyValueObject(x.Key, x.hp, x.strength, x.vitality, x.intelligence, x.wisdom,
                    x.agility, x.luck, x.WeakPoints, x.Skills))
                .First();
        }

        public ImmutableList<PropertyValueObject> Get(IList<CharacterTypeId> key)
        {
            throw new NotImplementedException();
        }

        public ImmutableList<PropertyValueObject> GetAll()
        {
            return _battleSceneScriptableObject.GetPropertyScriptableObject()
                .Select(x => new PropertyValueObject(x.Key, x.hp, x.strength, x.vitality, x.intelligence, x.wisdom,
                    x.agility, x.luck, x.WeakPoints, x.Skills))
                .ToImmutableList();
        }
    }
}
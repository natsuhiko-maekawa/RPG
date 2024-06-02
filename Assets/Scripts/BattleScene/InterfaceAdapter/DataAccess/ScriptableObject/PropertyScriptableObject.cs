using System.Collections.Generic;
using BattleScene.Infrastructure.Factory.Dto;
using UnityEngine;

namespace BattleScene.Infrastructure.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Property")]
    public class PropertyScriptableObject : UnityEngine.ScriptableObject
    {
        public List<PropertyDataStore> propertyList = new();
    }
}
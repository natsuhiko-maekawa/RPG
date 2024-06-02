using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.Framework.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Property")]
    public class PropertyScriptableObject : UnityEngine.ScriptableObject
    {
        public List<PropertyDataStore> propertyList = new();
    }
}
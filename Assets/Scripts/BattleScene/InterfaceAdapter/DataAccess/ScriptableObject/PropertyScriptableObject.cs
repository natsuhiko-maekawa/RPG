using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Property")]
    public class PropertyScriptableObject : UnityEngine.ScriptableObject
    {
        public List<PropertyDto> propertyList = new();
    }
}
using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Property")]
    public class PropertyScriptableObject : ScriptableObject
    {
        public List<PropertyDto> propertyList = new();
    }
}
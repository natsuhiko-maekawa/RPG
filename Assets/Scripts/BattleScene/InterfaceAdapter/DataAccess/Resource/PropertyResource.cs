using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class PropertyResource : MonoBehaviour, IPropertyResource
    {
        [SerializeField] private PropertyScriptableObject propertyScriptableObject;
        
        public List<PropertyDto> Get()
        {
            return propertyScriptableObject.propertyList;
        }
    }
}
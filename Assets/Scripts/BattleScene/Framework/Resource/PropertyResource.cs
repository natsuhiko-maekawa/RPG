using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    public class PropertyResource : MonoBehaviour, IPropertyResource
    {
        [SerializeField] private PropertyScriptableObject propertyScriptableObject;
        
        public List<PropertyDto> Get()
        {
            return propertyScriptableObject.propertyList;
        }
    }
    
    [CreateAssetMenu(menuName = "ScriptableObjects/Property")]
    public class PropertyScriptableObject : ScriptableObject
    {
        public List<PropertyDto> propertyList = new();
    }
}
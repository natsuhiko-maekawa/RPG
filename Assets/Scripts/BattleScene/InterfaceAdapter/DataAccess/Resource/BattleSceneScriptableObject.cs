using System;
using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [Obsolete]
    public class BattleSceneScriptableObject : MonoBehaviour, IBattleSceneScriptableObject
    {
        [FormerlySerializedAs("msgScriptableObject")] [SerializeField] private MessageScriptableObject messageScriptableObject;
        [SerializeField] private PropertyScriptableObject propertyScriptableObject;
        
        public List<MessageDto> GetMsgScriptableObject()
        {
            throw new NotImplementedException();
        }

        public List<PropertyDto> GetPropertyScriptableObject()
        {
            return propertyScriptableObject.propertyList;
        }
    }
}
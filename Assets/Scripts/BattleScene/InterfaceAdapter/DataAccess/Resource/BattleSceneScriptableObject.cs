using System;
using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [Obsolete]
    public class BattleSceneScriptableObject : MonoBehaviour, IBattleSceneScriptableObject
    {
        [SerializeField] private MsgScriptableObject msgScriptableObject;
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
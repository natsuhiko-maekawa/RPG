using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    public class BattleSceneScriptableObject : MonoBehaviour, IBattleSceneScriptableObject
    {
        [SerializeField] private MsgScriptableObject msgScriptableObject;
        [SerializeField] private PropertyScriptableObject propertyScriptableObject;

        public List<MessageDto> GetMsgScriptableObject()
        {
            return msgScriptableObject.msgList;
        }

        public List<PropertyDto> GetPropertyScriptableObject()
        {
            return propertyScriptableObject.propertyList;
        }
    }
}
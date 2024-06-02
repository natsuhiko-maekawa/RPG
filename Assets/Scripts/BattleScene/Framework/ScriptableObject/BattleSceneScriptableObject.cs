using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IScriptableObject;
using UnityEngine;

namespace BattleScene.Framework.ScriptableObject
{
    public class BattleSceneScriptableObject : MonoBehaviour, IBattleSceneScriptableObject
    {
        [SerializeField] private MsgScriptableObject msgScriptableObject;
        [SerializeField] private PropertyScriptableObject propertyScriptableObject;

        public List<MessageDto> GetMsgScriptableObject()
        {
            return msgScriptableObject.msgList;
        }

        public List<PropertyDataStore> GetPropertyScriptableObject()
        {
            return propertyScriptableObject.propertyList;
        }
    }
}
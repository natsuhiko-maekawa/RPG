using System.Collections.Generic;
using BattleScene.Infrastructure.Factory.Dto;
using BattleScene.Infrastructure.IScriptableObject;
using UnityEngine;

namespace BattleScene.Infrastructure.ScriptableObject
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
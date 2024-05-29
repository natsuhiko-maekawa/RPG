using System.Collections.Generic;
using BattleScene.Infrastructure.Factory.Dto;
using UnityEngine;

namespace BattleScene.Infrastructure.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Msg")]
    public class MsgScriptableObject : UnityEngine.ScriptableObject
    {
        public List<MessageDto> msgList = new();
    }
}
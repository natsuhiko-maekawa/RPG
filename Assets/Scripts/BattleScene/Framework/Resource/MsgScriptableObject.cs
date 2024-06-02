using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Msg")]
    public class MsgScriptableObject : ScriptableObject
    {
        public List<MessageDto> msgList = new();
    }
}
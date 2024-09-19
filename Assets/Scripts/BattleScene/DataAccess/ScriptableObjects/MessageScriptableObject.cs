using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Message")]
    public class MessageScriptableObject : BaseScriptableObject<MessageDto, MessageCode>
    {
    }
}
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Message")]
    public class MessageScriptableObject : BaseScriptableObject<MessageDto, MessageCode>
    {
    }
}
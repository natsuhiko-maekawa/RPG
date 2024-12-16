using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Message")]
    public class MessageScriptableObject : BaseScriptableObject<MessageDto, MessageCode>
    {
    }
}
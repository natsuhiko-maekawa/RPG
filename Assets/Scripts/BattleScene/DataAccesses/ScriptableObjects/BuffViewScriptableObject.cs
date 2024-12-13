using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuffView")]
    public class BuffViewScriptableObject : BaseScriptableObject<BuffViewDto, BuffCode>
    {
    }
}
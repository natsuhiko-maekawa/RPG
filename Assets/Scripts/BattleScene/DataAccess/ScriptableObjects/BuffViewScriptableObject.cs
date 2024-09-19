using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuffView")]
    public class BuffViewScriptableObject : BaseScriptableObject<BuffViewDto, BuffCode>
    {
    }
}
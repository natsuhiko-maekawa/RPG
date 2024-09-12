using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuffView")]
    public class BuffViewScriptableObject : BaseScriptableObject<BuffViewDto, BuffCode>
    {
    }
}
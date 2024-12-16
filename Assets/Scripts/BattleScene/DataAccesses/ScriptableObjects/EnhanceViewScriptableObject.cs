using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnhanceView")]
    public class EnhanceViewScriptableObject : BaseScriptableObject<EnhanceViewDto, EnhanceCode>
    {
    }
}
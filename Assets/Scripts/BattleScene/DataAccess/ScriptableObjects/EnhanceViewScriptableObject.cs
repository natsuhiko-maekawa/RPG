using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnhanceView")]
    public class EnhanceViewScriptableObject : BaseScriptableObject<EnhanceViewDto, EnhanceCode>
    {
    }
}
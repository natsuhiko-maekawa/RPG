using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentView")]
    public class AilmentViewScriptableObject : BaseScriptableObject<AilmentViewDto, (AilmentCode, SlipCode)>
    {
    }
}
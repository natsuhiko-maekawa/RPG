using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentView")]
    public class AilmentViewScriptableObject : BaseScriptableObject<AilmentViewDto, (AilmentCode, SlipCode)>
    {
    }
}
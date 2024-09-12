using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentView")]
    public class AilmentViewScriptableObject : BaseScriptableObject<AilmentViewDto, (AilmentCode, SlipDamageCode)>
    {
    }
}
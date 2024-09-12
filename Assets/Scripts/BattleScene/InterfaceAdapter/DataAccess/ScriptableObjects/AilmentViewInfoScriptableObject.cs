using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentViewInfo")]
    public class AilmentViewInfoScriptableObject : BaseScriptableObject<AilmentViewInfoDto, (AilmentCode, SlipDamageCode)>
    {
    }
}
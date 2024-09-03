using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentViewInfo")]
    public class AilmentViewInfoScriptableObject : BaseListScriptableObject<AilmentViewInfoDto, AilmentCode>
    {
    }
}
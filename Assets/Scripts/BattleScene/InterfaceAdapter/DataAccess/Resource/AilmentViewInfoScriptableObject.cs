using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentViewInfo")]
    public class AilmentViewInfoScriptableObject : BaseListScriptableObject<AilmentViewInfoDto, AilmentCode>
    {
        // [SerializeField] private List<AilmentViewInfoDto> ailmentViewInfoDtoList = new();
    }
}
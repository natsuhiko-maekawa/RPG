using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentViewInfo")]
    public class AilmentViewInfoScriptableObject : ScriptableObject
    {
        public List<AilmentViewInfoDto> ailmentViewInfoDtoList = new();
    }
}
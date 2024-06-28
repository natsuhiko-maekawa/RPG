using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyViewInfo")]
    public class EnemyViewInfoScriptableObject : ScriptableObject
    {
        public List<EnemyViewInfoDto> enemyViewInfoDtoList = new();
    }
}
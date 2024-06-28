using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyViewInfo")]
    public class EnemyViewInfoScriptableObject : ScriptableObject
    {
        public List<EnemyViewInfoDto> enemyViewInfoDtoList = new();
    }
}
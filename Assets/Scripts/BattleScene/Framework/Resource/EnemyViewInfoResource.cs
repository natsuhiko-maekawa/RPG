using System.Collections.Immutable;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    public class EnemyViewInfoResource : MonoBehaviour, IEnemyViewInfoResource
    {
        [SerializeField] private EnemyViewInfoScriptableObject enemyViewInfoScriptableObject;
        
        public ImmutableList<EnemyViewInfoDto> Get()
        {
            return enemyViewInfoScriptableObject.enemyViewInfoDtoList.ToImmutableList();
        }
    }
}
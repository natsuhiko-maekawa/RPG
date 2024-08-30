using System;
using System.Collections.Immutable;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class EnemyViewInfoResource : MonoBehaviour, IEnemyViewInfoResource
    {
        [SerializeField] private EnemyViewInfoScriptableObject enemyViewInfoScriptableObject;
        
        public ImmutableList<EnemyViewInfoDto> Get()
        {
            throw new NotImplementedException();
        }
    }
}
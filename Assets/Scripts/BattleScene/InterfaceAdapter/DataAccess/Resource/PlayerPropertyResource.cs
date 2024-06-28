using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class PlayerPropertyResource : MonoBehaviour, IPlayerPropertyResource
    {
        [SerializeField] private PlayerPropertyScriptableObject playerPropertyScriptableObject;
        
        public List<PlayerPropertyDto> Get()
        {
            return playerPropertyScriptableObject.playerPropertyList;
        }
    }
}
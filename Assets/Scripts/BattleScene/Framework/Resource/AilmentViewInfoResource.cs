using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    public class AilmentViewInfoResource : MonoBehaviour, IAilmentViewInfoResource
    {
        [SerializeField] private AilmentViewInfoScriptableObject ailmentViewInfoScriptableObject;
        
        public List<AilmentViewInfoDto> Get()
        {
            return ailmentViewInfoScriptableObject.ailmentViewInfoDtoList;
        }
    }
}
using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerProperty")]
    public class PlayerPropertyScriptableObject : ScriptableObject
    {
        public List<PlayerPropertyDto> playerPropertyList = new();
    }
}
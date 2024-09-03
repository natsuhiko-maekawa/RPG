using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.Factory;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class PlayerPropertyResource
        : BaseListScriptableObjectResource<PlayerPropertyScriptableObject, PlayerPropertyDto, CharacterTypeCode>
    {
    }
}
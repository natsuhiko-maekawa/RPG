﻿using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentProperty")]
    public class AilmentPropertyScriptableObject : BaseListScriptableObject<AilmentPropertyDto, AilmentCode>
    {
    }
}
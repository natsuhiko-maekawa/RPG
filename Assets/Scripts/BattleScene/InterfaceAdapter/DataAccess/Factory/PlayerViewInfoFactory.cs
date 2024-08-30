﻿using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.Resource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class PlayerViewInfoFactory
        : BaseListScriptableObjectFactory<PlayerViewInfoListScriptableObject, PlayerViewInfoDto, CharacterTypeId>
    {
    }
}
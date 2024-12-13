﻿using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class Confusion : BaseAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Confusion;
    }
}
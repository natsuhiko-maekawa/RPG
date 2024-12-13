﻿using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class Defence : BaseEnhance
    {
        public override EnhanceCode EnhanceCode { get; } = EnhanceCode.Defence;
        public override int Turn { get; } = 1;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToNextAction;
    }
}
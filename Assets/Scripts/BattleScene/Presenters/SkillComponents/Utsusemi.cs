﻿using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class Utsusemi : BaseEnhance
    {
        public override EnhanceCode EnhanceCode { get; } = EnhanceCode.Utsusemi;
        public override int Turn { get; } = 1;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToEndTurn;
    }
}
﻿using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Shichishitou : BaseDamage
    {
        public override int AttackNumber { get; } = 7;
        public override float DamageRate { get; } = 1.0f / 7.0f;
    }
}
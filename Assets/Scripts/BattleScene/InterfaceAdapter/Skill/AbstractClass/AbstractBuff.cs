﻿using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.Skill.AbstractClass
{
    public abstract class AbstractBuff
    {
        public abstract BuffCode BuffCode { get; }
        public virtual float Rate { get; } = 1.0f;
        public abstract int Turn { get; }
        public abstract LifetimeCode LifetimeCode { get; }
        
        public abstract BuffCode GetBuff();
        public abstract float GetBuffRate();
        public abstract int GetTurn();
        public abstract LifetimeCode GetLifetimeCode();
    }
}
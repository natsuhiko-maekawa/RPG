﻿using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.Skill.AbstractClass
{
    // TODO: Destroyに名前を変更
    public abstract class AbstractDestroyPart
    {
        public abstract BodyPartCode BodyPartCode { get; }
        public virtual float LuckRate { get; } = 0.5f;
        public virtual int Count { get; } = 1;
        
        public float GetLuckRate()
        {
            return 0.5f;
        }

        public abstract BodyPartCode GetDestroyPart();
    }
}
﻿using System.Collections.Immutable;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Interface
{
    public interface IDamageResult
    {
        public ImmutableList<AttackValueObject> AttackList { get; }
        public int GetTotal();
    }
}
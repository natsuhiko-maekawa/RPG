﻿using System;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    [Obsolete]
    public class TurnEntity
    {
        private int _turn;

        public void Increment()
        {
            ++_turn;
        }

        public TurnNumber Get()
        {
            return new TurnNumber(_turn);
        }
    }
}
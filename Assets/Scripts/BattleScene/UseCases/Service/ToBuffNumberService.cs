using System;
using BattleScene.Domain.Code;

namespace BattleScene.UseCases.Service
{
    public class ToBuffNumberService
    {
        public int Buff(BuffCode buffCode)
        {
            return buffCode switch
            {
                BuffCode.Attack => 0,
                BuffCode.Defence => 1,
                BuffCode.HitRate => 2,
                BuffCode.Avoidance => 3,
                BuffCode.Speed => 4,
                _ => throw new InvalidCastException()
            };
        }
    }
}
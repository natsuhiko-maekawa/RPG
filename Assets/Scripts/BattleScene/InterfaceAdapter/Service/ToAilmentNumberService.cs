using System;
using BattleScene.Domain.Code;
using static BattleScene.Domain.Code.SlipDamageCode;
using static BattleScene.Domain.Code.AilmentCode;

namespace BattleScene.InterfaceAdapter.Service
{
    [Obsolete]
    internal class ToAilmentNumberService
    {
        public int Ailment(AilmentCode ailmentCode)
        {
            return ailmentCode switch
            {
                Blind => 0,
                Deaf => 1,
                Confusion => 2,
                Paralysis => 3,
                Sleep => 4,
                Stun => 5,
                Petrifaction => 6,
                Curse => 13,
                Binding => 14,
                EnemyBlind => 0,
                EnemyDeaf => 1,
                EnemyParalysis => 3,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public int SlipDamage(SlipDamageCode slipDamageCode)
        {
            return slipDamageCode switch
            {
                Burning => 7,
                Freeze => 8,
                ElectricShock => 9,
                Poisoning => 10,
                Bleeding => 11,
                Suffocation => 12,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
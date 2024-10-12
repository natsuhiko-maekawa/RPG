using System.Collections.Generic;
using BattleScene.Domain.Code;
using static BattleScene.Domain.Code.SlipDamageCode;
using static BattleScene.Domain.Code.AilmentCode;

namespace BattleScene.InterfaceAdapter.Service
{
    internal class ToIndexService
    {
        private readonly IReadOnlyDictionary<AilmentCode, int> _ailmentDictionary = new Dictionary<AilmentCode, int>()
        {
            { Blind, 0 },
            { Deaf, 1 },
            { Confusion, 2 },
            { Paralysis, 3 },
            { Sleep, 4 },
            { Stun, 5 },
            { Petrifaction, 6 },
            { Curse, 13 },
            { Binding, 14 },
            { EnemyBlind, 0 },
            { EnemyDeaf, 1 },
            { EnemyParalysis, 3 }
        };

        private readonly IReadOnlyDictionary<SlipDamageCode, int> _slipDamageDictionary
            = new Dictionary<SlipDamageCode, int>()
            {
                { Burning, 7 },
                { Freeze, 8 },
                { ElectricShock, 9 },
                { Poisoning, 10 },
                { Bleeding, 11 },
                { Suffocation, 12 },
            };

        private readonly IReadOnlyDictionary<BodyPartCode, int> _bodyPartDictionary
            = new Dictionary<BodyPartCode, int>()
            {
                { BodyPartCode.Arm, 0 },
                { BodyPartCode.Leg, 1 },
                { BodyPartCode.Stomach, 2 }
            };

        private readonly IReadOnlyDictionary<BuffCode, int> _buffDictionary
            = new Dictionary<BuffCode, int>()
            {
                { BuffCode.Attack, 0 },
                { BuffCode.Defence, 1 },
                { BuffCode.HitRate, 2 },
                { BuffCode.Avoidance, 3 },
                { BuffCode.Speed, 4 }
            };

        public int FromAilment(AilmentCode ailmentCode) => _ailmentDictionary[ailmentCode];
        public int FromSlipDamage(SlipDamageCode slipDamageCode) => _slipDamageDictionary[slipDamageCode];
        public int FromBodyPart(BodyPartCode bodyPartCode) => _bodyPartDictionary[bodyPartCode];
        public int FromBuff(BuffCode buffCode) => _buffDictionary[buffCode];
    }
}
using System;
using BattleScene.Domain.Code;

namespace BattleScene.Infrastructure.Factory.Dto
{
    [Serializable]
    public class SlipDamageDto
    {
        public SlipDamageCode slipDamageCode;
        public int intervalTurn;
        public float damageRate;
    }
}
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.OldId
{
    public class SlipDamageId : AbstractId<SlipDamageId, HashId>
    {
        protected override HashId Id { get; }

        public SlipDamageId(
            SlipDamageCode slipDamageCode)
        {
            Id = new HashId(slipDamageCode);
        }
    }
}
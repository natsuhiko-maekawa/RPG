using BattleScene.Domain.Code;
using BattleScene.Infrastructure.Factory.Dto;

namespace BattleScene.Infrastructure.IScriptableObject
{
    public interface ISlipDamageScriptableObject
    {
        public SlipDamageDto Get(SlipDamageCode slipDamageCode);
    }
}
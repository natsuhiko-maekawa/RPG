using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public interface ISlipDamageScriptableObject
    {
        public SlipDamageDto Get(SlipDamageCode slipDamageCode);
    }
}
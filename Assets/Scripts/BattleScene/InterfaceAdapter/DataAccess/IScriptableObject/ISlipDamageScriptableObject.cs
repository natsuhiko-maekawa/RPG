using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IScriptableObject
{
    public interface ISlipDamageScriptableObject
    {
        public SlipDamageDto Get(SlipDamageCode slipDamageCode);
    }
}
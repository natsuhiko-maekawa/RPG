using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter
{
    public interface IAilmentViewInfoResource
    {
        public AilmentViewInfoDto Get(AilmentCode key);
        public AilmentViewInfoDto Get(SlipDamageCode key);
    }
}
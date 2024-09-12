using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter
{
    public interface IAilmentViewResource
    {
        public AilmentViewDto Get(AilmentCode key);
        public AilmentViewDto Get(SlipDamageCode key);
    }
}
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;

namespace BattleScene.DataAccess
{
    public interface IAilmentViewResource
    {
        public AilmentViewDto Get(AilmentCode key);
        public AilmentViewDto Get(SlipDamageCode key);
    }
}
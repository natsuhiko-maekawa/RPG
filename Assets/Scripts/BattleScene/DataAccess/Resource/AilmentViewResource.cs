using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Code;

namespace BattleScene.DataAccess.Resource
{
    public class AilmentViewResource
        : BaseScriptableObjectResource<AilmentViewScriptableObject, AilmentViewDto, (AilmentCode, SlipDamageCode)>,
            IAilmentViewResource
    {
        public AilmentViewDto Get(AilmentCode key) => Get((key, SlipDamageCode.NoSlipDamage));
        public AilmentViewDto Get(SlipDamageCode key) => Get((AilmentCode.NoAilment, key));
    }
}
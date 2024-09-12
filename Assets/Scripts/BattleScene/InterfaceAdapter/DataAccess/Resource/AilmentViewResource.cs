using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class AilmentViewResource
        : BaseScriptableObjectResource<AilmentViewScriptableObject, AilmentViewDto, (AilmentCode, SlipDamageCode)>,
            IAilmentViewResource
    {
        public AilmentViewDto Get(AilmentCode key) => Get((key, SlipDamageCode.NoSlipDamage));
        public AilmentViewDto Get(SlipDamageCode key) => Get((AilmentCode.NoAilment, key));
    }
}
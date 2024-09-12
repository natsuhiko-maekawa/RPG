using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class AilmentViewInfoScriptableObjectResource
        : BaseScriptableObjectResource<AilmentViewInfoScriptableObject, AilmentViewInfoDto, (AilmentCode,
            SlipDamageCode)>, IAilmentViewInfoResource
    {
        public AilmentViewInfoDto Get(AilmentCode key) => Get((key, SlipDamageCode.NoSlipDamage));
        public AilmentViewInfoDto Get(SlipDamageCode key) => Get((AilmentCode.NoAilment, key));
    }
}
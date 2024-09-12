using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObject;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class AilmentViewInfoListScriptableObjectResource
        : BaseListScriptableObjectResource<AilmentViewInfoScriptableObject, AilmentViewInfoDto, (AilmentCode, SlipDamageCode)>
    {
        public AilmentViewInfoDto Get(AilmentCode key) => Get((key, SlipDamageCode.NoSlipDamage));
        public AilmentViewInfoDto Get(SlipDamageCode key) => Get((AilmentCode.NoAilment, key));
    }
}
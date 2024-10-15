using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Code;

namespace BattleScene.DataAccess.Resource
{
    public class AilmentViewResource
        : BaseScriptableObjectResource<AilmentViewScriptableObject, AilmentViewDto, (AilmentCode, SlipCode)>,
            IResource<AilmentViewDto, AilmentCode, SlipCode>
    {
        public AilmentViewDto Get(AilmentCode key) => Get((key, SlipCode.NoSlip));
        public AilmentViewDto Get(SlipCode key) => Get((AilmentCode.NoAilment, key));
    }
}
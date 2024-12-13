using BattleScene.DataAccesses.Dto;
using BattleScene.DataAccesses.ScriptableObjects;
using BattleScene.Domain.Codes;

namespace BattleScene.DataAccesses.Resource
{
    public class AilmentViewResource
        : BaseScriptableObjectResource<AilmentViewScriptableObject, AilmentViewDto, (AilmentCode, SlipCode)>,
            IResource<AilmentViewDto, AilmentCode, SlipCode>
    {
        public AilmentViewDto Get(AilmentCode key) => Get((key, SlipCode.NoSlip));
        public AilmentViewDto Get(SlipCode key) => Get((AilmentCode.NoAilment, key));
    }
}
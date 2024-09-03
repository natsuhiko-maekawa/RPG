using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.Resource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class AilmentViewInfoListScriptableObjectResource
        : BaseListScriptableObjectResource<AilmentViewInfoScriptableObject, AilmentViewInfoDto, AilmentCode>
    {
    }
}
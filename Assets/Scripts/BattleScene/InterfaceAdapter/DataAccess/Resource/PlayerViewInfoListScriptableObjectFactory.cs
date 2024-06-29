using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class PlayerViewInfoListScriptableObjectFactory
        : BaseListScriptableObjectFactory<PlayerViewInfoListScriptableObject, PlayerViewInfoValueObject, PlayerImageCode>
    {
    }
}
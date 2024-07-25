using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class PlayerViewInfoListScriptableObjectFactory
        : BaseListScriptableObjectFactory<PlayerViewInfoListScriptableObject, PlayerImageValueObject, PlayerImageCode>
    {
    }
}
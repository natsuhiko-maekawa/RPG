using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObject;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class PlayerImagePathResource
        : BaseListScriptableObjectResource<PlayerImageListScriptableObject, PlayerImageValueObject, PlayerImageCode>
    {
    }
}
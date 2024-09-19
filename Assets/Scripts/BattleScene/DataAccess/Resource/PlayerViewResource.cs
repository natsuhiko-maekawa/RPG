using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Code;

namespace BattleScene.DataAccess.Resource
{
    public class PlayerViewResource
        : BaseScriptableObjectResource<PlayerViewScriptableObject, PlayerViewDto, CharacterTypeCode>
    {
    }
}
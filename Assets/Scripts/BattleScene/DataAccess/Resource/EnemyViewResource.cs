using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Codes;

namespace BattleScene.DataAccess.Resource
{
    public class EnemyViewResource
        : BaseScriptableObjectResource<EnemyViewScriptableObject, EnemyViewDto, CharacterTypeCode>
    {
    }
}
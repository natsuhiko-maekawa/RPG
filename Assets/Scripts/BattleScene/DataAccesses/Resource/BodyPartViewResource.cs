using BattleScene.DataAccesses.Dto;
using BattleScene.DataAccesses.ScriptableObjects;
using BattleScene.Domain.Codes;

namespace BattleScene.DataAccesses.Resource
{
    public class BodyPartViewResource
        : BaseScriptableObjectResource<BodyPartViewScriptableObject, BodyPartViewDto, BodyPartCode>
    {
    }
}
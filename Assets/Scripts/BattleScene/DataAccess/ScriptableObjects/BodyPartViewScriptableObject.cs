using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BodyPartView")]
    public class BodyPartViewScriptableObject : BaseScriptableObject<BodyPartViewDto, BodyPartCode>
    {
    }
}
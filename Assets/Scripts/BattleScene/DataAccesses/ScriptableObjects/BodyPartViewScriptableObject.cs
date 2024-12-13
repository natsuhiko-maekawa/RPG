using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BodyPartView")]
    public class BodyPartViewScriptableObject : BaseScriptableObject<BodyPartViewDto, BodyPartCode>
    {
    }
}
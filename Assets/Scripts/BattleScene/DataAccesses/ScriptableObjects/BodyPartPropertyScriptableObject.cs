using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BodyPartProperty")]
    public class BodyPartPropertyScriptableObject : BaseScriptableObject<BodyPartPropertyDto, BodyPartCode>
    {
    }
}
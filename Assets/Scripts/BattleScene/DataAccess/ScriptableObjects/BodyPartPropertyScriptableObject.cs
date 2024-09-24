using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BodyPartProperty")]
    public class BodyPartPropertyScriptableObject : BaseScriptableObject<BodyPartPropertyDto, BodyPartCode>
    {
    }
}
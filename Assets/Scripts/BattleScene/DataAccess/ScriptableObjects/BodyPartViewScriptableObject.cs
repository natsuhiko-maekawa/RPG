using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BodyPartView")]
    public class BodyPartViewScriptableObject : BaseScriptableObject<BodyPartViewDto, BodyPartCode>
    {
    }
}
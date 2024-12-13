using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SkillView")]
    public class SkillViewScriptableObject : BaseScriptableObject<SkillViewDto, SkillCode>
    {
    }
}
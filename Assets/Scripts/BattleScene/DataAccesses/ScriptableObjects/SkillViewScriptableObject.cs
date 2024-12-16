using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SkillView")]
    public class SkillViewScriptableObject : BaseScriptableObject<SkillViewDto, SkillCode>
    {
    }
}
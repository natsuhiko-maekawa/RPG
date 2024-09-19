using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SkillView")]
    public class SkillViewScriptableObject : BaseScriptableObject<SkillPropertyDto, SkillCode>
    {
    }
}
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SkillView")]
    public class SkillViewScriptableObject : BaseScriptableObject<SkillPropertyDto, SkillCode>
    {
    }
}
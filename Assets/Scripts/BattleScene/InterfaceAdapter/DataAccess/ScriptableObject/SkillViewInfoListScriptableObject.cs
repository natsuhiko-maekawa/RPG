using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SkillViewInfo")]
    public class SkillViewInfoListScriptableObject : BaseListScriptableObject<SkillViewInfoValueObject, SkillCode>
    {
    }
}
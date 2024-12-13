using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentProperty")]
    public class AilmentPropertyScriptableObject : BaseScriptableObject<AilmentPropertyDto, AilmentCode>
    {
    }
}
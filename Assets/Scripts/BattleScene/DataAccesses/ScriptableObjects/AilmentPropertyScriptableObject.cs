using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AilmentProperty")]
    public class AilmentPropertyScriptableObject : BaseScriptableObject<AilmentPropertyDto, AilmentCode>
    {
    }
}
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyView")]
    public class EnemyViewScriptableObject : BaseScriptableObject<EnemyViewDto, CharacterTypeCode>
    {
    }
}
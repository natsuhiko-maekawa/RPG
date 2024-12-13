using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyView")]
    public class EnemyViewScriptableObject : BaseScriptableObject<EnemyViewDto, CharacterTypeCode>
    {
    }
}
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyView")]
    public class EnemyViewScriptableObject : BaseScriptableObject<EnemyViewDto, CharacterTypeCode>
    {
    }
}
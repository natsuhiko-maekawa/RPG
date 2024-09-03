using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyViewInfo")]
    public class EnemyViewInfoScriptableObject : BaseListScriptableObject<EnemyViewInfoDto, CharacterTypeCode>
    {
    }
}
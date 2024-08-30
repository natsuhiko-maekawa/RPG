using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyViewInfo")]
    public class EnemyViewInfoScriptableObject : BaseListScriptableObject<EnemyViewInfoValueObject, CharacterTypeCode>
    {
    }
}
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyViewInfo")]
    public class EnemyViewInfoScriptableObject : BaseListScriptableObject<EnemyViewInfoValueObject, CharacterTypeCode>
    {
    }
}
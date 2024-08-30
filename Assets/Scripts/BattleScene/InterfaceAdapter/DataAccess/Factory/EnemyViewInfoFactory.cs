using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Resource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class EnemyViewInfoFactory 
        : BaseListScriptableObjectFactory<EnemyViewInfoScriptableObject, EnemyViewInfoValueObject, CharacterTypeCode>
    {
    }
}
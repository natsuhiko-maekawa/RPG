using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class AilmentFactory : IAilmentFactory
    {
        private readonly IAilmentScriptableObject _ailmentScriptableObject;

        public AilmentFactory(
            IAilmentScriptableObject ailmentScriptableObject)
        {
            _ailmentScriptableObject = ailmentScriptableObject;
        }

        public AilmentEntity Create(CharacterId characterId, AilmentCode ailmentCode)
        {
            var ailmentDto = _ailmentScriptableObject.Get(ailmentCode);
            return new AilmentEntity(
                characterId,
                ailmentCode,
                ailmentDto.priority,
                new TurnValueObject(ailmentDto.effectiveTurn));
        }
    }
}
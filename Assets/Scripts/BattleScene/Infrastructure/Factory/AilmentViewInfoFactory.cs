using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.Infrastructure.IScriptableObject;

namespace BattleScene.Infrastructure.Factory
{
    public class AilmentViewInfoFactory : IAilmentViewInfoFactory
    {
        private readonly IAilmentViewInfoScriptableObject _ailmentViewInfoScriptableObject;

        public AilmentViewInfoFactory(
            IAilmentViewInfoScriptableObject ailmentViewInfoScriptableObject)
        {
            _ailmentViewInfoScriptableObject = ailmentViewInfoScriptableObject;
        }
        
        public AilmentViewInfoValueObject Create(AilmentCode ailmentCode)
        {
            var ailmentViewInfoDto = _ailmentViewInfoScriptableObject.Select(ailmentCode);
            return new AilmentViewInfoValueObject(
                ailmentCode: ailmentCode,
                ailmentName: ailmentViewInfoDto.ailmentName,
                messageCode: ailmentViewInfoDto.messageCode,
                playerImageCode: ailmentViewInfoDto.playerImageCode);
        }
    }
}
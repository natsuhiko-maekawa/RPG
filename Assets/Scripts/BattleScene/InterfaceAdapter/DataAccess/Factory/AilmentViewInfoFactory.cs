using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.IResource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
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
                ailmentCode,
                ailmentViewInfoDto.ailmentName,
                ailmentViewInfoDto.messageCode,
                ailmentViewInfoDto.playerImageCode);
        }
    }
}
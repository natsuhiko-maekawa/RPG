using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.IResource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class AilmentViewInfoFactory : IAilmentViewInfoFactory
    {
        private readonly IAilmentViewInfoResource _ailmentViewInfoResource;

        public AilmentViewInfoFactory(
            IAilmentViewInfoResource ailmentViewInfoResource)
        {
            _ailmentViewInfoResource = ailmentViewInfoResource;
        }

        public AilmentViewInfoValueObject Create(AilmentCode ailmentCode)
        {
            var ailmentViewInfoDto = _ailmentViewInfoResource.Get()
                .First(x => x.AilmentCode == ailmentCode);
            return new AilmentViewInfoValueObject(
                ailmentCode,
                ailmentViewInfoDto.ailmentName,
                ailmentViewInfoDto.MessageCode,
                ailmentViewInfoDto.PlayerImageCode);
        }
    }
}
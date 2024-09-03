using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Resource;

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
                .First(x => x.Key == ailmentCode);
            return new AilmentViewInfoValueObject(
                ailmentCode,
                ailmentViewInfoDto.AilmentName,
                ailmentViewInfoDto.MessageCode,
                ailmentViewInfoDto.PlayerImageCode);
        }
    }
}
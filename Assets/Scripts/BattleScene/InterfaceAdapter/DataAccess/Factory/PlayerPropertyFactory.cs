using System.Linq;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.IResource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class PlayerPropertyFactory : IPlayerPropertyFactory
    {
        private readonly IPlayerPropertyResource _playerPropertyResource;

        public PlayerPropertyFactory(
            IPlayerPropertyResource playerPropertyResource)
        {
            _playerPropertyResource = playerPropertyResource;
        }

        public PlayerPropertyValueObject Get()
        {
            return _playerPropertyResource.Get()
                .Select(x => new PlayerPropertyValueObject(x.technicalPoint, x.FatalitySkills))
                .First();
        }
    }
}
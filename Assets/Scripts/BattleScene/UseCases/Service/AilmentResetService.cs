using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.Service
{
    public class AilmentResetService
    {
        private readonly PlayerDomainService _player;
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;

        public AilmentResetService(
            PlayerDomainService player,
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection)
        {
            _player = player;
            _ailmentCollection = ailmentCollection;
        }

        public void Reset(AilmentCode ailmentCode)
        {
            var playerId = _player.GetId();
            _ailmentCollection.Get((playerId, ailmentCode)).Effects = false;
        }
    }
}
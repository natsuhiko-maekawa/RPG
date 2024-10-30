using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class AilmentResetService : IAilmentResetService
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

        public void Reset(CharacterId targetId, AilmentCode ailmentCode)
        {
            _ailmentCollection.Get((targetId, ailmentCode)).Effects = false;
        }

        public void Reset(ILookup<CharacterId, AilmentCode> ailmentLookup)
        {
            var query = ailmentLookup
                .SelectMany(ailmentGroup => ailmentGroup
                    .Select(ailmentCode => (TargetId: ailmentGroup.Key, AilmentCode: ailmentCode)));
            foreach (var (targetId, ailmentCode) in query) 
                Reset(targetId, ailmentCode);
        }
    }
}
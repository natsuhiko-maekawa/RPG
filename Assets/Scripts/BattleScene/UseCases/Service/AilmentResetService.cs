using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class AilmentResetService
    {
        private readonly PlayerDomainService _player;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;

        public AilmentResetService(
            PlayerDomainService player,
            OrderedItemsDomainService orderedItems,
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection)
        {
            _player = player;
            _orderedItems = orderedItems;
            _ailmentCollection = ailmentCollection;
        }

        public void Reset()
        {
            _orderedItems.First().TryGetAilmentCode(out var ailmentCode);
            MyDebug.Assert(ailmentCode != AilmentCode.NoAilment);
            var playerId = _player.GetId();
            _ailmentCollection.Get((playerId, ailmentCode)).Effects = false;
        }
    }
}
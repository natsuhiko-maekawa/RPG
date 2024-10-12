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
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;

        public AilmentResetService(
            PlayerDomainService player,
            OrderedItemsDomainService orderedItems,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository)
        {
            _player = player;
            _orderedItems = orderedItems;
            _ailmentRepository = ailmentRepository;
        }

        public void Reset()
        {
            _orderedItems.First().TryGetAilmentCode(out var ailmentCode);
            MyDebug.Assert(ailmentCode != AilmentCode.NoAilment);
            var playerId = _player.GetId();
            _ailmentRepository.Select((playerId, ailmentCode)).Effects = false;
        }
    }
}
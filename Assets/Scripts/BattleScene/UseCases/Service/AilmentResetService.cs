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
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;

        public AilmentResetService(
            PlayerDomainService player,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository)
        {
            _player = player;
            _ailmentRepository = ailmentRepository;
        }

        public void Reset(AilmentCode ailmentCode)
        {
            var playerId = _player.Get().Id;
            _ailmentRepository.Get((playerId, ailmentCode)).Effects = false;
        }

        public void Reset(CharacterId targetId, AilmentCode ailmentCode)
        {
            _ailmentRepository.Get((targetId, ailmentCode)).Effects = false;
        }

        public void Reset(ILookup<CharacterId, AilmentCode> ailmentLookup)
        {
            var query = ailmentLookup
                .SelectMany(ailmentGroup => ailmentGroup
                    .Select(ailmentCode => (CharacterId: ailmentGroup.Key, AilmentCode: ailmentCode)));
            foreach (var (characterId, ailmentCode) in query) 
                Reset(characterId, ailmentCode);
        }
    }
}
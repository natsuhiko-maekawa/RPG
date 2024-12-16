using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;

namespace BattleScene.Domain.DomainServices
{
    public class BattleLogDomainService
    {
        private readonly IRepository<BattleEventEntity, BattleEventId> _battleLogRepository;

        public BattleLogDomainService(
            IRepository<BattleEventEntity, BattleEventId> battleLogRepository)
        {
            _battleLogRepository = battleLogRepository;
        }

        public BattleEventEntity GetLast()
        {
            return _battleLogRepository.Get().Max();
        }
    }
}
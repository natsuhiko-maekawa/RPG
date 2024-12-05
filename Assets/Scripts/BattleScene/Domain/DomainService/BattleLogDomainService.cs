using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DomainService
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
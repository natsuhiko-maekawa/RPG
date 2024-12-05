using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DomainService
{
    public class BattleLogDomainService
    {
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;

        public BattleLogDomainService(
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository)
        {
            _battleLogRepository = battleLogRepository;
        }

        public BattleLogEntity GetLast()
        {
            return _battleLogRepository.Get().Max();
        }
    }
}
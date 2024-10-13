using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DomainService
{
    public class BattleLogDomainService
    {
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;

        public BattleLogDomainService(
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection)
        {
            _battleLogCollection = battleLogCollection;
        }

        public BattleLogEntity GetLast()
        {
            return _battleLogCollection.Get().Max();
        }
    }
}
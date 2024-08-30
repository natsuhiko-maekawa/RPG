using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class HitPointDomainService
    {
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;

        public HitPointDomainService(
            IRepository<HitPointAggregate, CharacterId> hitPointRepository)
        {
            _hitPointRepository = hitPointRepository;
        }

        public bool AnyIsDead()
        {
            return _hitPointRepository.Select()
                .Any(x => !x.IsSurvive());
        }
    }
}
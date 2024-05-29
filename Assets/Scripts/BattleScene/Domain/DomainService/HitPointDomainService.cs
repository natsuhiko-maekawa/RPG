using System.Linq;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class HitPointDomainService
    {
        private readonly IHitPointRepository _hitPointRepository;

        public HitPointDomainService(
            IHitPointRepository hitPointRepository)
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
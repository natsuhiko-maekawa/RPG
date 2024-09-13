using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class SlipDomainService
    {
        private readonly IRepository<SlipDamageEntity, SlipDamageCode> _slipRepository;

        public SlipDomainService(
            IRepository<SlipDamageEntity, SlipDamageCode> slipRepository)
        {
            _slipRepository = slipRepository;
        }

        public void AdvanceTurn()
        {
            var slip = _slipRepository.Select()
                .Select(x =>
                {
                    x.AdvanceTurn();
                    return x;
                })
                .ToImmutableList();
            _slipRepository.Update(slip);
        }
    }
}
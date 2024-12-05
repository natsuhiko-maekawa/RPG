using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.DomainService
{
    public class SlipDomainService
    {
        private readonly IRepository<SlipEntity, SlipCode> _slipRepository;

        public SlipDomainService(
            IRepository<SlipEntity, SlipCode> slipRepository)
        {
            _slipRepository = slipRepository;
        }

        public void AdvanceTurn()
        {
            var slip = _slipRepository.Get()
                .Select(x =>
                {
                    x.AdvanceTurn();
                    return x;
                })
                .ToList();
            _slipRepository.Add(slip);
        }
    }
}
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;

namespace BattleScene.Domain.DomainServices
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
            foreach (var slip in _slipRepository.Get())
            {
                slip.AdvanceTurn();
            }
        }
    }
}
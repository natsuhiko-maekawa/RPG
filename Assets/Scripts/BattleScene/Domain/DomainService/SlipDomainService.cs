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
            foreach (var slip in _slipRepository.Get())
            {
                slip.AdvanceTurn();
            }
        }
    }
}
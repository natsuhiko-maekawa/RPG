using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class SlipResetService : ISlipResetService
    {
        private readonly IRepository<SlipEntity, SlipCode> _slipRepository;

        public SlipResetService(
            IRepository<SlipEntity, SlipCode> slipRepository)
        {
            _slipRepository = slipRepository;
        }

        public void Reset(ILookup<CharacterId, SlipCode> slipLookup)
        {
            foreach (var grouping in slipLookup)
            {
                foreach (var slipCode in grouping)
                {
                    _slipRepository.Get(slipCode).Effects = false;
                }
            }
        }
    }
}
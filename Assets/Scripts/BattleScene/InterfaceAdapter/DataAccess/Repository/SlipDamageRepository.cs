using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class SlipDamageRepository : ISlipDamageRepository
    {
        public ImmutableList<SlipDamageEntity> Select()
        {
            throw new System.NotImplementedException();
        }

        public SlipDamageEntity Select(SlipDamageCode slipDamageCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
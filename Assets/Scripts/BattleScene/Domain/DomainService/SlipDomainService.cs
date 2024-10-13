using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.DomainService
{
    public class SlipDomainService
    {
        private readonly ICollection<SlipEntity, SlipDamageCode> _slipCollection;

        public SlipDomainService(
            ICollection<SlipEntity, SlipDamageCode> slipCollection)
        {
            _slipCollection = slipCollection;
        }

        public void AdvanceTurn()
        {
            var slip = _slipCollection.Get()
                .Select(x =>
                {
                    x.AdvanceTurn();
                    return x;
                })
                .ToImmutableList();
            _slipCollection.Add(slip);
        }
    }
}
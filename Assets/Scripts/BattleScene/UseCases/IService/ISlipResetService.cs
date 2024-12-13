using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Ids;

namespace BattleScene.UseCases.IService
{
    public interface ISlipResetService
    {
        public void Reset(ILookup<CharacterId, SlipCode> slipLookup);
    }
}
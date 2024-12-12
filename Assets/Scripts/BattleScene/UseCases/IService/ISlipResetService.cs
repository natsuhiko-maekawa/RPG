using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IService
{
    public interface ISlipResetService
    {
        public void Reset(ILookup<CharacterId, SlipCode> slipLookup);
    }
}
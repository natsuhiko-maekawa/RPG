using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IService
{
    public interface IAilmentResetService
    {
        public void Reset(AilmentCode ailmentCode);
        public void Reset(ILookup<CharacterId, AilmentCode> ailmentLookup);
    }
}
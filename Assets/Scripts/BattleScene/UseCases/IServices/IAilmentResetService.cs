using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Ids;

namespace BattleScene.UseCases.IServices
{
    public interface IAilmentResetService
    {
        public void Reset(AilmentCode ailmentCode);
        public void Reset(ILookup<CharacterId, AilmentCode> ailmentLookup);
    }
}
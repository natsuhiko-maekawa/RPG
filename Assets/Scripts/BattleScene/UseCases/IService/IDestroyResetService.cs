using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Ids;

namespace BattleScene.UseCases.IService
{
    public interface IDestroyResetService
    {
        public void Reset(ILookup<CharacterId, BodyPartCode> bodyPartLookup);
    }
}
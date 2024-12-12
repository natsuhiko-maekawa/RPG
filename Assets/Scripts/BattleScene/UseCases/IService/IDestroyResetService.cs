using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IService
{
    public interface IDestroyResetService
    {
        public void Reset(ILookup<CharacterId, BodyPartCode> bodyPartLookup);
    }
}
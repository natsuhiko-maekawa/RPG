using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IResource
{
    public interface IAilmentScriptableObject
    {
        public AilmentDto Get(AilmentCode ailmentCode);
    }
}
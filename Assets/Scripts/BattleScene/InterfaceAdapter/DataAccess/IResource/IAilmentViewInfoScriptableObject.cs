using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IResource
{
    public interface IAilmentViewInfoScriptableObject
    {
        public AilmentViewInfoDto Select(AilmentCode ailmentCode);
    }
}
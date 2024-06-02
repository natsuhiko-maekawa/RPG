using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IScriptableObject
{
    public interface IAilmentScriptableObject
    {
        public AilmentDto Get(AilmentCode ailmentCode);
    }
}
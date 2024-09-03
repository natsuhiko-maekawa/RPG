using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public interface IAilmentScriptableObject
    {
        public AilmentDto Get(AilmentCode ailmentCode);
    }
}
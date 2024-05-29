using BattleScene.Domain.Code;
using BattleScene.Infrastructure.Factory.Dto;

namespace BattleScene.Infrastructure.IScriptableObject
{
    public interface IAilmentScriptableObject
    {
        public AilmentDto Get(AilmentCode ailmentCode);
    }
}
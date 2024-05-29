using BattleScene.Domain.Code;
using BattleScene.Infrastructure.Factory.Dto;

namespace BattleScene.Infrastructure.IScriptableObject
{
    public interface IAilmentViewInfoScriptableObject
    {
        public AilmentViewInfoDto Select(AilmentCode ailmentCode);
    }
}
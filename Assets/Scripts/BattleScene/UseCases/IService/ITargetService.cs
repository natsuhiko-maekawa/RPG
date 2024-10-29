using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IService
{
    public interface ITargetService
    {
        public IReadOnlyList<CharacterId> Get(CharacterId actorId, Range range);
    }
}
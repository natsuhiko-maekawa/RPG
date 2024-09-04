using System.Collections.Generic;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IPresenter
{
    public interface ITargetViewPresenter
    {
        public void Start(IList<CharacterId> targetIdList);
    }
}
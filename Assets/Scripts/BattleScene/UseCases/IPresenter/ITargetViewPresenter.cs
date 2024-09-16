using System;
using System.Collections.Generic;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IPresenter
{
    [Obsolete]
    public interface ITargetViewPresenter
    {
        public void Start(IList<CharacterId> targetIdList);
        public void Stop();
    }
}
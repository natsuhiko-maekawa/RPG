using System.Collections.Generic;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.BuffView;
using BattleScene.InterfaceAdapter.Presenter.DestroyedPartView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IPlayerStatusView
    {
        public void StartPlayerAilmentsView(IList<PlayerAilmentsViewDto> dtoList);
        public void StartPlayerDestroyedPartView(IList<PlayerDestroyedPartViewDto> dtoList);
        public void StartPlayerBuffView(IList<BuffViewDto> dtoList);
    }
}
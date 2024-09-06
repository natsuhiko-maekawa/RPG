using System.Collections.Generic;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.BuffView;
using BattleScene.InterfaceAdapter.Presenter.DestroyedPartView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IPlayerStatusView
    {
        public void StartPlayerAilmentsView(PlayerAilmentsViewDto dto);
        public void StartPlayerDestroyedPartView(IList<PlayerDestroyedPartViewDto> dtoList);
        public void StartPlayerBuffView(IList<BuffViewDto> dtoList);
    }
}
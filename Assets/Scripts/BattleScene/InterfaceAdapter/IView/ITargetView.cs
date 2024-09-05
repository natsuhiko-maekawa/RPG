using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface ITargetView
    {
        public Task StartAnimation(TargetViewDto dto);
        public void StopAnimation();
        public void SetSelectAction(Action<ImmutableList<CharacterDto>> action);
    }
}
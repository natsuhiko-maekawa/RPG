using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface ITargetView
    {
        public Task StartAnimation(TargetViewDto dto);
    }
}
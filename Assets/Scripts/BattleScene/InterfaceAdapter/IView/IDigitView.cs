using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.DigitView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IDigitView
    {
        public Task StartAnimation(IList<DigitDto> dtoList);
    }
}
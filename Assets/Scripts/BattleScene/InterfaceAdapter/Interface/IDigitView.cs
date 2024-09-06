using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.DigitView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IDigitView
    {
        public Task StartAnimation(IList<DigitDto> dtoList);
    }
}
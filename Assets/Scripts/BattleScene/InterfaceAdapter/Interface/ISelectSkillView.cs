using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.SelectSkillView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface ISelectSkillView
    {
        public Task StartAnimation(SelectSkillViewDto selectSkillViewDto);
        public void StopAnimation();
    }
}
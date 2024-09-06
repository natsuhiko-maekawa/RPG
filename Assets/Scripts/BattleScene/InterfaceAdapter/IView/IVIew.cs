using System.Threading.Tasks;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IVIew<TDto>
    {
        public Task StartAnimationAsync(TDto dto);
        public void StopAnimation();
    }
}
using System.Threading.Tasks;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IVIew<TDto>
    {
        public Task StartAnimationAsync(TDto dto);
        public void StopAnimation();
    }
}
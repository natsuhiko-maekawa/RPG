using System.Threading.Tasks;

namespace LoadingScene.UseCase.IRepository
{
    public interface ISpriteRepository
    {
        public float GetProgress(int addressableCount);
        public Task LoadImage();
    }
}
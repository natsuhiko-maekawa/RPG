using System.Threading.Tasks;

namespace LoadingScene.Domain
{
    public interface ILoader
    {
        public Task Awake();
        public void Update();
    }
}
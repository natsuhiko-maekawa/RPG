using LoadingScene.UseCase.IService;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace LoadingScene.Presenter
{
    public class LoadingScene : IStartable, ITickable
    {
        private ILoader _loader = null!;

        void IStartable.Start()
        {
            Application.targetFrameRate = 60;
            _loader.Awake();
        }

        void ITickable.Tick()
        {
            _loader.Update();
            SceneManager.LoadScene("Scenes/BattleScene");
        }

        [Inject]
        public void Construct(ILoader loader)
        {
            _loader = loader;
        }
    }
}
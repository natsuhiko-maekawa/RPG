using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace LoadingScene.Domain
{
    public class LoadingScene : MonoBehaviour
    {
        private ILoader _loader;
        private Task _task;
        
        [Inject]
        public void Construct(ILoader loader)
        {
            _loader = loader;
        }
        
        private void Awake()
        {
            Application.targetFrameRate = 60;
            _task = _loader.Awake();    
        }

        private void Update()
        {
            _loader.Update();
            if (_task.IsCompleted) SceneManager.LoadScene("Scenes/BattleScene");
        }
    }
}
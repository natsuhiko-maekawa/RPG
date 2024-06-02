using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.UserInterface.View
{
    public class FrameView : MonoBehaviour
    {
        [SerializeField] private Image frame;
        private Image _frame;

        private void Awake()
        {
            _frame = Instantiate(frame, transform);
            _frame.enabled = false;
        }

        public Task StartAnimation(Color color)
        {
            _frame.color = color;
            _frame.enabled = true;
            return Task.CompletedTask;
        }

        public void StopAnimation()
        {
            _frame.enabled = false;
        }
    }
}
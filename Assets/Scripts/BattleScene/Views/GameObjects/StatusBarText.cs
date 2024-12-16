using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.GameObjects
{
    public class StatusBarText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void Set(int currentPoint, int maxPoint) => _text.text = currentPoint + "/" + maxPoint;
    }
}
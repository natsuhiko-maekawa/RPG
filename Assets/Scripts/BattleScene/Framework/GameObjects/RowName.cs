using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.GameObjects
{
    public class RowName : MonoBehaviour
    {
        private Text _rowName;

        private void Awake()
        {
            _rowName = GetComponent<Text>();
            Hide();
        }

        public void Set(string rowName) => _rowName.text = rowName;
        public void Show() => _rowName.enabled = true;
        public void Hide() => _rowName.enabled = false;
        
    }
}
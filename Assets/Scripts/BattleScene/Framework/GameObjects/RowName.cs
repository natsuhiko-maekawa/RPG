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
            enabled = false;
        }

        private void OnEnable()
        {
            _rowName.enabled = true;
        }

        public void Set(string rowName) => _rowName.text = rowName;

        private void OnDisable()
        {
            _rowName.enabled = false;
        }
    }
}
using TMPro;
using UnityEngine;

namespace BattleScene.Views.GameObjects
{
    public class RowName : MonoBehaviour
    {
        public Color defaultColor;
        public Color highlightColor;
        private TMP_Text _rowName;

        private void Awake()
        {
            _rowName = GetComponent<TextMeshProUGUI>();
            enabled = false;
        }

        private void Reset()
        {
            defaultColor = Color.white;
            highlightColor = Color.red;
        }

        private void OnEnable()
        {
            _rowName.enabled = true;
        }

        public void Set(string rowName) => _rowName.text = rowName;
        public void Highlight() => _rowName.color = highlightColor;
        public void Unhighlight() => _rowName.color = defaultColor;

        private void OnDisable()
        {
            _rowName.enabled = false;
        }
    }
}
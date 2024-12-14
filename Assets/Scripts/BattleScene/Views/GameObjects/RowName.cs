using Common;
using TMPro;
using UnityEngine;

namespace BattleScene.Views.GameObjects
{
    public class RowName : MonoBehaviour
    {
        public Color defaultColor;
        public Color highlightColor;
        public Color inactiveColor;
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
            inactiveColor = Color.gray;
        }

        private void OnEnable()
        {
            _rowName.enabled = true;
        }

        public void Set(string rowName) => _rowName.text = rowName;
        public void Highlight() => _rowName.color = highlightColor;
        public void Unhighlight() => _rowName.color = defaultColor;
        public void Inactive() => _rowName.color = inactiveColor;

        private void OnDisable()
        {
            _rowName.enabled = false;
        }
    }
}
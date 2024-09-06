using TMPro;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI technicalPoint;
        [SerializeField] private Color gray = new(0.5215687f, 0.5215687f, 0.5215687f);
        [SerializeField] private Color blue = new(0.5215687f, 0.5215687f, 0.9803922f);
        [SerializeField] private Color lightBlue = new(0.5215687f, 0.9215686f, 0.9803922f);
        [SerializeField] private Color red = new(0.9803922f, 0.5215687f, 0.5215687f);
        [SerializeField] private Color lightRed = new(0.9803922f, 0.9215686f, 0.5215687f);
        private GridView _gridView;

        private void Awake()
        {
            _gridView = GetComponent<GridView>();
        }
    }
}
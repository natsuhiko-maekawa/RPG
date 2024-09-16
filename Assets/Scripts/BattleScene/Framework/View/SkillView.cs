using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Code;
using BattleScene.InterfaceAdapter.Presenter.Dto;
using TMPro;
using UnityEngine;

namespace BattleScene.Framework.View
{
    [Obsolete]
    public class SkillView : MonoBehaviour
    {
        // [SerializeField] private TextMeshProUGUI technicalPoint;
        [SerializeField] private Color white = new(0.9803922f, 0.9803922f, 0.9803922f);
        [SerializeField] private Color gray = new(0.5215687f, 0.5215687f, 0.5215687f);
        [SerializeField] private Color blue = new(0.5215687f, 0.5215687f, 0.9803922f);
        [SerializeField] private Color lightBlue = new(0.5215687f, 0.9215686f, 0.9803922f);
        [SerializeField] private Color red = new(0.9803922f, 0.5215687f, 0.5215687f);
        [SerializeField] private Color lightRed = new(0.9803922f, 0.9215686f, 0.5215687f);
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly List<TextMeshProUGUI> _technicalPointList = new();
        private ObsoleteGridView _obsoleteGridView;

        private void Awake()
        {
            _obsoleteGridView = GetComponent<ObsoleteGridView>();
        }

        public Task StartAnimationAsync(SkillViewDto dto)
        {
            var gridState = _obsoleteGridView.GridStateDictionary[ActionCode.Skill];
            foreach (var index in Enumerable.Range(0, _obsoleteGridView.MaxGridSize))
            {
                _technicalPointList[index].text
                    = dto.SkillRowDtoList[index + gridState.TopItemIndex].TechnicalPoint.ToString();
                if (dto.SkillRowDtoList[index + gridState.TopItemIndex].Enabled)
                    _technicalPointList[index].colorGradient = new VertexGradient(white, white, gray, gray);
                else if (index == gridState.SelectedRow)
                    _technicalPointList[index].colorGradient = new VertexGradient(lightRed, lightRed, red, red);
                else
                    _technicalPointList[index].colorGradient = new VertexGradient(lightBlue, lightBlue, blue, blue);
                _technicalPointList[index].enabled = true;
            }
            return Task.CompletedTask;
        }

        public void StopAnimation()
        {
        }
    }
}
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.OutputData;

namespace BattleScene.UseCases.View
{
    public class SkillView
    {
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly PlayerDomainService _player;
        private readonly IViewPresenter<SkillViewOutputData> _skillView;

        public void Start()
        {
            var skillCodeList = _characterPropertyFactory.Create(CharacterTypeCode.Player).Skills;
            var skillRowList = skillCodeList
                .Select(x =>
                {
                    var technicalPoint = _skillFactory.Create(x).TechnicalPoint;
                    var enabled = technicalPoint < _player.Get().CurrentTechnicalPoint;
                    return new SkillRow(
                        SkillCode: x,
                        TechnicalPoint: technicalPoint,
                        Enabled: enabled);
                })
                .ToImmutableList();
            var skillViewOutputData = new SkillViewOutputData(skillRowList);
            _skillView.Start(skillViewOutputData);
        }

        public void Stop()
        {
            _skillView.Stop();
        }
    }
}
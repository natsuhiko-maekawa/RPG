using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.Dto;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.OutputData;
using ActionCode = BattleScene.InterfaceAdapter.Code.ActionCode;

namespace BattleScene.InterfaceAdapter.Presenter.ViewPresenter
{
    public class SkillViewPresenter : IViewPresenter<SkillViewOutputData>
    {
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly IResource<SkillViewInfoValueObject, SkillCode> _skillPropertyFactory;
        private readonly IGridView _gridView;
        private readonly IVIew<SkillViewDto> _skillView;

        public SkillViewPresenter(
            IResource<MessageDto, MessageCode> messageResource,
            IResource<SkillViewInfoValueObject, SkillCode> skillPropertyFactory,
            IGridView gridView,
            IVIew<SkillViewDto> skillView)
        {
            _messageResource = messageResource;
            _skillPropertyFactory = skillPropertyFactory;
            _gridView = gridView;
            _skillView = skillView;
        }

        public void Start(SkillViewOutputData outputData)
        {
            var rowDtoList = outputData.Row
                .Select((x, i) =>
                {
                    var skillProperty = _skillPropertyFactory.Get(x.SkillCode);
                    var skillName = skillProperty.SkillName;
                    var description = _messageResource.Get(skillProperty.Description).Message;
                    var playerImagePath = GetPlayerImagePath(skillProperty.PlayerImageCode);
                    return new RowDto(
                        RowId: i,
                        RowName: skillName,
                        RowDescription: description,
                        PlayerImagePath: playerImagePath,
                        Enabled: x.Enabled);
                })
                .ToImmutableList();
            var gridViewDto = new GridViewDto(
                ActionCode: ActionCode.Skill,
                rowDtoList);
            _gridView.StartAnimationAsync(gridViewDto);
            
            var skillRowList = outputData.Row
                .Select((x, i) => new SkillRowDto(
                    RowId: i,
                    TechnicalPoint: x.TechnicalPoint))
                .ToImmutableList();
            var skillDto = new SkillViewDto(skillRowList);
            _skillView.StartAnimationAsync(skillDto);
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
        
        private string GetPlayerImagePath(PlayerImageCode playerImageCode)
        {
            return $"{playerImageCode}[{playerImageCode}]";
        }
    }
}
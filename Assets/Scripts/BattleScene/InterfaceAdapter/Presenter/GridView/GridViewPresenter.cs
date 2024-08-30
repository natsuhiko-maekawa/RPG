using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.View;
using BattleScene.UseCases.View.GridView;

namespace BattleScene.InterfaceAdapter.Presenter.GridView
{
    public class GridViewPresenter : IViewPresenter<GridViewOutputData>
    {
        private readonly IFactory<MessageDto, MessageCode> _messageFactory;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly IGridView _gridView;
        
        public void Start(GridViewOutputData outputData)
        {
            var rowList = outputData.Row
                .Select(x =>
                {
                    var rowName = x.ToString();
                    var rowDescription = GetDescription(x.ActionCode);
                    var enabled = x.Enabled;
                    return new RowDto(
                        RowName: rowName,
                        RowDescription: rowDescription,
                        Enabled: enabled);
                })
                .ToImmutableList();
            var dto = new GridViewDto(rowList);
            _gridView.StartAnimationAsync(dto);
        }

        private string GetDescription(ActionCode actionCode)
        {
            var messageCode = actionCode switch
            {
                ActionCode.Attack => MessageCode.AttackDescription,
                ActionCode.Skill => MessageCode.SkillDescription,
                ActionCode.Defence => MessageCode.DefenceDescription,
                ActionCode.FatalitySkill => MessageCode.FatalitySkillDescription,
                _ => MessageCode.NoMessage
            };

            var message = _messageFactory.Create(messageCode).Message;
            message = _messageCodeConverter.Replace(message);
            return message;
        }
    }
}
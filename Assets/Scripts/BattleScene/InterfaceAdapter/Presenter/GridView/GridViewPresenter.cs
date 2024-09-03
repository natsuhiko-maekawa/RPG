using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.View;
using BattleScene.UseCases.View.GridView;

namespace BattleScene.InterfaceAdapter.Presenter.GridView
{
    public class GridViewPresenter : IViewPresenter<GridViewOutputData>
    {
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly IGridView _gridView;

        public GridViewPresenter(
            IResource<MessageDto, MessageCode> messageResource,
            MessageCodeConverterService messageCodeConverter,
            IGridView gridView)
        {
            _messageResource = messageResource;
            _messageCodeConverter = messageCodeConverter;
            _gridView = gridView;
        }

        public void Start(GridViewOutputData outputData)
        {
            var rowList = outputData.Row
                .Select(x =>
                {
                    var rowName = x.ActionCode.ToString();
                    var rowDescription = GetDescription(x.ActionCode);
                    var enabled = x.Enabled;
                    return new RowDto(
                        RowId: (int)x.ActionCode,
                        RowName: rowName,
                        RowDescription: rowDescription,
                        PlayerImagePath: GetPlayerImagePath(x.ActionCode),
                        Enabled: enabled);
                })
                .ToImmutableList();
            var dto = new GridViewDto(rowList);
            _gridView.StartAnimationAsync(dto);
        }

        public void Stop()
        {
            _gridView.StopAnimation();
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

            var message = _messageResource.Get(messageCode).Message;
            message = _messageCodeConverter.Replace(message);
            return message;
        }

        private string GetPlayerImagePath(ActionCode actionCode)
        {
            return $"{actionCode}[{actionCode}]";
        }
    }
}
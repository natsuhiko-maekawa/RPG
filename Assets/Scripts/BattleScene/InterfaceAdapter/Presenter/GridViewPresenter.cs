using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Framework.View;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.Presenter.Dto;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.ViewPresenter
{
    public class GridViewPresenter : IViewPresenter<GridViewOutputData>
    {
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly IResource<PlayerImagePathDto, PlayerImageCode> _playerImagePathResource;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly GridView _gridView;

        public GridViewPresenter(
            IResource<MessageDto, MessageCode> messageResource,
            IResource<PlayerImagePathDto, PlayerImageCode> playerImagePathResource,
            MessageCodeConverterService messageCodeConverter,
            GridView gridView)
        {
            _messageResource = messageResource;
            _playerImagePathResource = playerImagePathResource;
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
                        // TODO: 仮で刀のイラストを設定している
                        PlayerImagePath: _playerImagePathResource.Get(PlayerImageCode.Katana).PlayerImagePath,
                        Enabled: enabled);
                })
                .ToImmutableList();
            var dto = new GridViewDto(
                ActionCode: Code.ActionCode.Action,
                rowList);
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
    }
}
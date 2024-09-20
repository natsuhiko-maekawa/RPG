using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class GridViewPresenter
    {
        private readonly AttackCounterService _attackCounter;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly IResource<PlayerImagePathDto, PlayerImageCode> _playerImagePathResource;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly GridView _gridView;

        public GridViewPresenter(
            AttackCounterService attackCounter,
            IResource<MessageDto, MessageCode> messageResource,
            IResource<PlayerImagePathDto, PlayerImageCode> playerImagePathResource,
            MessageCodeConverterService messageCodeConverter,
            GridView gridView)
        {
            _attackCounter = attackCounter;
            _messageResource = messageResource;
            _playerImagePathResource = playerImagePathResource;
            _messageCodeConverter = messageCodeConverter;
            _gridView = gridView;
        }

        public async void StartAnimationAsync()
        {
            var actionCodeList = ImmutableList.Create(
                ActionCode.Attack,
                ActionCode.Skill,
                ActionCode.Defence,
                ActionCode.FatalitySkill);
            
            var rowList = actionCodeList
                .Select(x =>
                {
                    var rowName = x.ToString();
                    var rowDescription = GetDescription(x);
                    var enabled = GetEnabled(x);
                    return new RowDto(
                        RowId: (int)x,
                        RowName: rowName,
                        RowDescription: rowDescription,
                        // TODO: 仮で刀のイラストを設定している
                        PlayerImagePath: _playerImagePathResource.Get(PlayerImageCode.Katana).PlayerImagePath,
                        Enabled: enabled);
                })
                .ToImmutableList();
            var dto = new GridViewDto(
                ActionCode: Framework.Code.ActionCode.Action,
                rowList);
            await _gridView.StartAnimationAsync(dto);
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

        private bool GetEnabled(ActionCode actionCode)
        {
            var enabled = actionCode switch
            {
                ActionCode.Attack or ActionCode.Skill or ActionCode.Defence => true,
                ActionCode.FatalitySkill => _attackCounter.IsOverflow(),
                _ => throw new ArgumentOutOfRangeException(nameof(actionCode), actionCode, null)
            };

            return enabled;
        }
    }
}
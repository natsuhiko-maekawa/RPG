using System;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IResource<PlayerImageDto, PlayerImageCode> _playerImagePathResource;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly GridView _gridView;

        public GridViewPresenter(
            AttackCounterService attackCounter,
            IResource<MessageDto, MessageCode> messageResource,
            IResource<PlayerImageDto, PlayerImageCode> playerImagePathResource,
            MessageCodeConverterService messageCodeConverter,
            GridView gridView)
        {
            _attackCounter = attackCounter;
            _messageResource = messageResource;
            _playerImagePathResource = playerImagePathResource;
            _messageCodeConverter = messageCodeConverter;
            _gridView = gridView;
        }

        public async Task StartAnimationAsync()
        {
            var actionCodeList = new[]
                { BattleEventCode.Attack, BattleEventCode.Skill, BattleEventCode.Defence, BattleEventCode.FatalitySkill };

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
                        PlayerImagePath: _playerImagePathResource.Get(PlayerImageCode.Katana).Path,
                        Enabled: enabled);
                })
                .ToList();
            var dto = new GridViewDto(
                ActionCode: Framework.Code.ActionCode.Action,
                rowList);
            await _gridView.StartAnimationAsync(dto);
        }

        public void Stop()
        {
            _gridView.StopAnimation();
        }

        private string GetDescription(BattleEventCode battleEventCode)
        {
            var messageCode = battleEventCode switch
            {
                BattleEventCode.Attack => MessageCode.AttackDescription,
                BattleEventCode.Skill => MessageCode.SkillDescription,
                BattleEventCode.Defence => MessageCode.DefenceDescription,
                BattleEventCode.FatalitySkill => MessageCode.FatalitySkillDescription,
                _ => MessageCode.NoMessage
            };

            var message = _messageResource.Get(messageCode).Message;
            message = _messageCodeConverter.Replace(message);
            return message;
        }

        private bool GetEnabled(BattleEventCode battleEventCode)
        {
            var enabled = battleEventCode switch
            {
                BattleEventCode.Attack or BattleEventCode.Skill or BattleEventCode.Defence => true,
                BattleEventCode.FatalitySkill => _attackCounter.IsOverflow(),
                _ => throw new ArgumentOutOfRangeException(nameof(battleEventCode), battleEventCode, null)
            };

            return enabled;
        }
    }
}
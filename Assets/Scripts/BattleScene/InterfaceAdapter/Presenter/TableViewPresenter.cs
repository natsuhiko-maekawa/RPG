using System;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Framework.Code;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class TableViewPresenter
    {
        private readonly AttackCounterService _attackCounter;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly IResource<PlayerImageDto, PlayerImageCode> _playerImagePathResource;
        private readonly TableView _tableView;
        private static readonly BattleEventCode[] BattleEventCodeList
            = { BattleEventCode.Attack, BattleEventCode.Skill, BattleEventCode.Defence, BattleEventCode.FatalitySkill };

        public TableViewPresenter(
            AttackCounterService attackCounter,
            IResource<MessageDto, MessageCode> messageResource,
            IResource<PlayerImageDto, PlayerImageCode> playerImagePathResource,
            TableView tableView)
        {
            _attackCounter = attackCounter;
            _messageResource = messageResource;
            _playerImagePathResource = playerImagePathResource;
            _tableView = tableView;
        }

        public void StartAnimation()
        {
            var rowList = BattleEventCodeList
                .Select(x =>
                {
                    var rowName = x.ToString();
                    var rowDescription = GetDescription(x);
                    var enabled = GetEnabled(x);
                    return new Row(
                        rowName: rowName,
                        rowDescription: rowDescription,
                        // TODO: 仮で刀のイラストを設定している。
                        playerImagePath: _playerImagePathResource.Get(PlayerImageCode.Katana).Path,
                        enabled: enabled,
                        technicalPoint: 0);
                })
                .ToList();
            var dto = new TableViewModel(
                actionCode: ActionCode.Action,
                rowList: rowList);
            _tableView.StartAnimationAsync(dto);
        }

        public void Stop()
        {
            _tableView.StopAnimation();
        }

        private string[] GetDescription(BattleEventCode battleEventCode)
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
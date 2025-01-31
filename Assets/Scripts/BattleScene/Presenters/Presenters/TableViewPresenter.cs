﻿using System;
using System.Linq;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.UseCases.Services;
using BattleScene.Views.Code;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;

namespace BattleScene.Presenters.Presenters
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
                    return new RowModel(
                        rowName: rowName,
                        rowDescription: rowDescription,
                        // TODO: 仮でイラストを設定している。
                        playerImagePath: _playerImagePathResource.Get(PlayerImageCode.NoImage).Path,
                        enabled: enabled,
                        technicalPoint: 0);
                })
                .ToList();
            var dto = new TableViewModel(
                actionCode: ActionCode.Action,
                rowList: rowList);
            _tableView.StartAnimation(dto);
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
                _ => throw new ArgumentOutOfRangeException(nameof(battleEventCode), battleEventCode, null)
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
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
    public class GridViewPresenter
    {
        private readonly AttackCounterService _attackCounter;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly IResource<PlayerImageDto, PlayerImageCode> _playerImagePathResource;
        private readonly GridView _gridView;
        private static readonly BattleEventCode[] BattleEventCodeList
            = { BattleEventCode.Attack, BattleEventCode.Skill, BattleEventCode.Defence, BattleEventCode.FatalitySkill };

        public GridViewPresenter(
            AttackCounterService attackCounter,
            IResource<MessageDto, MessageCode> messageResource,
            IResource<PlayerImageDto, PlayerImageCode> playerImagePathResource,
            GridView gridView)
        {
            _attackCounter = attackCounter;
            _messageResource = messageResource;
            _playerImagePathResource = playerImagePathResource;
            _gridView = gridView;
        }

        public void StartAnimationAsync()
        {
            var rowList = BattleEventCodeList
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
                ActionCode: ActionCode.Action,
                rowList);
            _gridView.StartAnimationAsync(dto);
        }

        public void Stop()
        {
            _gridView.StopAnimation();
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
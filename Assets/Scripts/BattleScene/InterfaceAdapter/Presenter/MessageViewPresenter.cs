using System.Linq;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class MessageViewPresenter
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly MessageView _messageView;

        public MessageViewPresenter(
            BattleLogDomainService battleLog, 
            OrderedItemsDomainService orderedItems, 
            IResource<MessageDto, MessageCode> messageResource, 
            MessageCodeConverterService messageCodeConverter, 
            MessageView messageView)
        {
            _battleLog = battleLog;
            _orderedItems = orderedItems;
            _messageResource = messageResource;
            _messageCodeConverter = messageCodeConverter;
            _messageView = messageView;
        }

        public async Task Start(MessageCode messageCode, bool noWait = false)
        {
            Debug.Assert(messageCode != MessageCode.NoMessage);
            _messageView.StopAnimation();
            var message = _messageResource.Get(messageCode).Message;
            message = _messageCodeConverter.Replace(message);
            await _messageView.StartAnimationAsync(new MessageViewDto(message, noWait));
        }

        public async void StartDamageMessageViewAsync()
        {
            var messageCode = GetDamageMessageCode();
            await Start(messageCode);
        }
        
        private MessageCode GetDamageMessageCode()
        {
            if (IsAvoid) return MessageCode.AvoidMessage;
            if (DamagesOneself) return MessageCode.DamageOneselfMessage;
            if (AttacksWeakPoint) return MessageCode.WeakPointMessage;
            if (DamagesTarget) return MessageCode.DamageMessage;
            return MessageCode.NoMessage;
        }

        private bool IsAvoid => _battleLog.GetLast().AttackList.All(x => !x.IsHit);
        private bool AttacksWeakPoint => _battleLog.GetLast().AttackList.Any(x => x.AttacksWeakPoint);
        private bool DamagesOneself => _battleLog.GetLast().AttackList.Any(x => IsOneself(x.TargetId) && !IsAvoid);
        private bool DamagesTarget => !DamagesOneself && !IsAvoid;

        private bool IsOneself(CharacterId characterId)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            Debug.Assert(actorId != null);
            var value = Equals(characterId, actorId);
            return value;
        }
    }
}
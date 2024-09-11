using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.UseCases.View.MessageView.OutputData;
using UnityEngine;

namespace BattleScene.UseCases.View.MessageView.OutputDataFactory
{
    public class DamageMessageOutputDataFactory
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly BattleLogDomainService _battleLog;

        public DamageMessageOutputDataFactory(
            MessageOutputDataFactory messageOutputDataFactory,
            OrderedItemsDomainService orderedItems,
            BattleLogDomainService battleLog)
        {
            _messageOutputDataFactory = messageOutputDataFactory;
            _orderedItems = orderedItems;
            _battleLog = battleLog;
        }

        public MessageOutputData Create()
        {
            var messageCode = GetMessageCode();
            Debug.Assert(messageCode != MessageCode.NoMessage);
            return _messageOutputDataFactory.Create(messageCode);
        }

        private MessageCode GetMessageCode()
        {
            if (IsAvoid()) return MessageCode.AvoidMessage;
            if (DamagesOneself()) return MessageCode.DamageMessage;
            if (AttacksWeakPoint()) return MessageCode.WeakPointMessage;
            if (DamagesTarget()) return MessageCode.DamageMessage;
            return MessageCode.NoMessage;
        }
        
        private bool IsAvoid() => _battleLog.GetLast().AttackList.All(x => !x.IsHit);
        private bool AttacksWeakPoint() => _battleLog.GetLast().AttackList.Any(x => x.AttacksWeakPoint);
        private bool DamagesOneself() 
            => _battleLog.GetLast().AttackList.Any(x => Equals(x.TargetId, GetActor())) && !IsAvoid();
        
        private bool DamagesTarget() => !DamagesOneself() && !IsAvoid();

        private CharacterId GetActor()
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) throw new InvalidOperationException();
            return actorId;
        }
    }
}
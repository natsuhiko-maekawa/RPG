using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.View.MessageView.OutputData;

namespace BattleScene.UseCases.View.MessageView.OutputDataFactory
{
    public class DamageMessageOutputDataFactory
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultDomainService _result;

        public DamageMessageOutputDataFactory(
            MessageOutputDataFactory messageOutputDataFactory,
            OrderedItemsDomainService orderedItems,
            ResultDomainService result)
        {
            _messageOutputDataFactory = messageOutputDataFactory;
            _orderedItems = orderedItems;
            _result = result;
        }

        public MessageOutputData Create()
        {
            if (_result.LastDamage().AttackList.Any(x => x.AttacksWeakPoint))
                return _messageOutputDataFactory.Create(MessageCode.WeakPointMessage);
            if (_result.LastDamage().AttackList
                .Any(x => !Equals(x.TargetId, _orderedItems.FirstCharacterId()) && x.IsHit))
                return _messageOutputDataFactory.Create(MessageCode.DamageMessage);
            if (_result.LastDamage().AttackList
                .Any(x => Equals(x.TargetId, _orderedItems.FirstCharacterId()) && x.IsHit))
                return _messageOutputDataFactory.Create(MessageCode.DamageOneselfMessage);
            return _messageOutputDataFactory.Create(MessageCode.AvoidMessage);
        }
    }
}
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.View.MessageView.OutputData;

namespace BattleScene.UseCases.View.MessageView.OutputDataFactory
{
    public class AilmentMessageOutputDataFactory
    {
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly OrderedItemsDomainService _orderedItems;

        public AilmentMessageOutputDataFactory(
            IAilmentViewInfoFactory ailmentViewInfoFactory,
            MessageOutputDataFactory messageOutputDataFactory,
            OrderedItemsDomainService orderedItems)
        {
            _ailmentViewInfoFactory = ailmentViewInfoFactory;
            _messageOutputDataFactory = messageOutputDataFactory;
            _orderedItems = orderedItems;
        }

        public MessageOutputData Create()
        {
            var ailmentCode = _orderedItems.FirstAilmentCode();
            var messageCode = _ailmentViewInfoFactory.Create(ailmentCode).MessageCode;
            return _messageOutputDataFactory.Create(messageCode);
        }
    }
}
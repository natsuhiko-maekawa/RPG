using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.UseCase.View.MessageView.OutputData;

namespace BattleScene.UseCase.View.MessageView.OutputDataFactory
{
    public class AilmentMessageOutputDataFactory
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;

        public MessageOutputData Create()
        {
            var ailmentCode = _orderedItems.FirstAilmentCode();
            var messageCode = _ailmentViewInfoFactory.Create(ailmentCode).MessageCode;
            return _messageOutputDataFactory.Create(messageCode);
        }
    }
}
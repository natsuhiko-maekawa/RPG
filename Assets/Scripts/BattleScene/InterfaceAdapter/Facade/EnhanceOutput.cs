using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class EnhanceOutput
    {
        private readonly IResource<EnhanceViewDto, EnhanceCode> _enhanceViewResource;
        private readonly MessageViewPresenter _messageView;

        public EnhanceOutput(
            MessageViewPresenter messageView,
            IResource<EnhanceViewDto, EnhanceCode> enhanceViewResource)
        {
            _messageView = messageView;
            _enhanceViewResource = enhanceViewResource;
        }

        public async Task Out(BattleEventValueObject enhance)
        {
            var messageCode = _enhanceViewResource.Get(enhance.EnhanceCode).MessageCode;
            await _messageView.StartAnimationAsync(messageCode);
        }
    }
}
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.UseCases.Output
{
    public class TargetView
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ITargetRepository _targetRepository;
        private readonly ITargetViewPresenter _targetView;
        
        public void Out()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var target = _targetRepository.Select(characterId);
            _targetView.Out(target);
        }
    }
}
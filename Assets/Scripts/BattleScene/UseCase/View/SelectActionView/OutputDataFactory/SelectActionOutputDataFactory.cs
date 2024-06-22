using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.SelectActionView.OutputData;

namespace BattleScene.UseCase.View.SelectActionView.OutputDataFactory
{
    public class SelectActionOutputDataFactory
    {
        private readonly AttackCounterService _attackCounter;
        private readonly ISelectorRepository _selectorRepository;

        public SelectActionOutputDataFactory(
            AttackCounterService attackCounter,
            ISelectorRepository selectorRepository)
        {
            _attackCounter = attackCounter;
            _selectorRepository = selectorRepository;
        }

        public SelectActionOutputData Create()
        {
            var selector = _selectorRepository.Select(new SelectorId(EventCode.SelectActionEvent)).GetSelector();
            var disabledRowList = _attackCounter.IsOverflow()
                ? new List<int>()
                : new List<int> { 3 };
            return new SelectActionOutputData(
                selector.ActualViewLength,
                selector.Selection,
                disabledRowList);
        }
    }
}
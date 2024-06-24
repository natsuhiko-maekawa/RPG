using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.SelectActionView.OutputData;

namespace BattleScene.UseCases.View.SelectActionView.OutputDataFactory
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
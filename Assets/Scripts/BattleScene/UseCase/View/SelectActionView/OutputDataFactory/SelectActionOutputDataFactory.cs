using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.SelectActionView.OutputData;

namespace BattleScene.UseCase.View.SelectActionView.OutputDataFactory
{
    public class SelectActionOutputDataFactory
    {
        private readonly AttackCounterService _attackCounter;
        private readonly CharactersDomainService _characters;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly ISelectorRepository _selectorRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;

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
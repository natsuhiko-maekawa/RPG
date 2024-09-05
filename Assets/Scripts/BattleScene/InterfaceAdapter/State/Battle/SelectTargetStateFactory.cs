using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SelectTargetStateFactory
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly SkillStateFactory _skillStateFactory;
        private readonly TargetDomainService _target;
        private readonly ITargetViewPresenter _targetView;

        public SelectTargetStateFactory(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            SkillStateFactory skillStateFactory,
            OrderedItemsDomainService orderedItems,
            TargetDomainService target,
            ITargetViewPresenter targetView)
        {
            _skillFactory = skillFactory;
            _skillStateFactory = skillStateFactory;
            _orderedItems = orderedItems;
            _target = target;
            _targetView = targetView;
        }

        public SelectTargetState Create(SkillCode skillCode)
        {
            return new SelectTargetState(
                skillFactory: _skillFactory,
                orderedItems: _orderedItems,
                skillStateFactory: _skillStateFactory,
                target: _target,
                targetView: _targetView,
                skillCode: skillCode);
        }
    }
}
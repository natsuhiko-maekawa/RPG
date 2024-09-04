using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SelectTargetState : AbstractState
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly TargetDomainService _target;
        private readonly ITargetViewPresenter _targetView;
        private readonly SkillCode _skillCode;

        public SelectTargetState(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            OrderedItemsDomainService orderedItems,
            TargetDomainService target,
            ITargetViewPresenter targetView,
            SkillCode skillCode)
        {
            _skillFactory = skillFactory;
            _orderedItems = orderedItems;
            _target = target;
            _targetView = targetView;
            _skillCode = skillCode;
        }

        public override void Start()
        {
            _orderedItems.First().TryGetCharacterId(out var characterId);
            var skill = _skillFactory.Create(_skillCode);
            var target = _target.Get(
                characterId: characterId,
                range: skill.SkillCommon.Range);
            _targetView.Start(target);
        }
    }
}
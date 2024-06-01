using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Event.TemplateMethod;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.AilmentView.OutputBoundary;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.DestroyedPartView.OutputBoundary;
using BattleScene.UseCase.View.DestroyedPartView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.Event.Runner.EventCode;

namespace BattleScene.UseCase.Event
{
    /// <summary>
    ///     異常回復スキルを実行し、リポジトリを更新するクラス
    /// </summary>
    internal class ResetSkillEvent : SkillEvent, IEvent
    {
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly IAilmentViewPresenter _ailmentViewPresenter;
        private readonly CharactersDomainService _characters;
        private readonly DestroyedPartOutputDataFactory _destroyedPartOutputDataFactory;
        private readonly IDestroyedPartViewPresenter _destroyedPartViewPresenter;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResetSkillService _resetSkill;
        private readonly ResultDomainService _result;
        private readonly IResultRepository _resultRepository;
        private readonly SkillCreatorService _skillCreator;
        private readonly ISkillRepository _skillRepository;

        public ResetSkillEvent(
            AilmentOutputDataFactory ailmentOutputDataFactory,
            IAilmentRepository ailmentRepository,
            IAilmentViewPresenter ailmentViewPresenter,
            CharactersDomainService characters,
            DestroyedPartOutputDataFactory destroyedPartOutputDataFactory,
            IDestroyedPartViewPresenter destroyedPartViewPresenter,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageViewPresenter,
            OrderedItemsDomainService orderedItems,
            ResetSkillService resetSkill,
            ResultDomainService result,
            IResultRepository resultRepository,
            SkillCreatorService skillCreator,
            ISkillRepository skillRepository)
        {
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentRepository = ailmentRepository;
            _ailmentViewPresenter = ailmentViewPresenter;
            _characters = characters;
            _destroyedPartOutputDataFactory = destroyedPartOutputDataFactory;
            _destroyedPartViewPresenter = destroyedPartViewPresenter;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageViewPresenter = messageViewPresenter;
            _orderedItems = orderedItems;
            _resetSkill = resetSkill;
            _result = result;
            _resultRepository = resultRepository;
            _skillCreator = skillCreator;
            _skillRepository = skillRepository;
        }

        protected override void UpdateResultRepository()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);
            skill.DequeSkillElement();
            _skillRepository.Update(skill);
        }

        protected override void UpdateSkillRepository()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);
            var result = _resetSkill.Execute(skill);
            _resultRepository.Update(result);
        }

        protected override EventCode RunSkillEvent()
        {
            var resetSkillResult = _result.Last<ResetSkillResultValueObject>();

            foreach (var targetId in resetSkillResult.TargetIdList)
            foreach (var ailmentCode in resetSkillResult.AilmentCodeList)
            {
                _ailmentRepository.Delete(targetId, ailmentCode);
                if (ailmentCode != AilmentCode.Confusion) continue;
                var skill = _skillCreator.Create(targetId, SkillCode.Attack);
                _skillRepository.Update(skill);
            }
            // TODO: 部位破壊とデバフのリセット処理を書く
            // TODO: 表示の処理を書く
            // var ailmentOutputData = _ailmentOutputDataFactory.Create();
            // _ailmentViewPresenter.Start(ailmentOutputData);
            // var destroyedPartOutputData = _destroyedPartOutputDataFactory.Create();
            // _destroyedPartViewPresenter.Start(destroyedPartOutputData);
            // var message = _messageOutputDataFactory.Create();
            // _messageViewPresenter.Start(message);

            return WaitEvent;
        }
    }
}
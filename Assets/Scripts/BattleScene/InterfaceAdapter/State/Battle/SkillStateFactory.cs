using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.State.Skill;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SkillStateFactory
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly AilmentStateFactory _ailmentStateFactory;
        private readonly BuffStateFactory _buffStateFactory;
        private readonly DamageStateFactory _damageStateFactory;
        private readonly RestoreStateFactory _restoreStateFactory;
        private readonly IMessageViewPresenter _messageView;
        
        public SkillStateFactory(
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            AilmentStateFactory ailmentStateFactory,
            BuffStateFactory buffStateFactory,
            DamageStateFactory damageStateFactory,
            RestoreStateFactory restoreStateFactory,
            IMessageViewPresenter messageView)
        {
            _container = container;
            _skillFactory = skillFactory;
            _ailmentStateFactory = ailmentStateFactory;
            _buffStateFactory = buffStateFactory;
            _damageStateFactory = damageStateFactory;
            _restoreStateFactory = restoreStateFactory;
            _messageView = messageView;
        }

        public SkillState Create(SkillCode skillCode, IList<CharacterId> targetIdList) => new(
            skillCode: skillCode,
            targetIdList: targetIdList,
            container: _container, 
            skillFactory: _skillFactory,
            ailmentStateFactory: _ailmentStateFactory,
            buffStateFactory: _buffStateFactory,
            damageStateFactory: _damageStateFactory,
            restoreStateFactory: _restoreStateFactory,
            messageView: _messageView);
    }
}
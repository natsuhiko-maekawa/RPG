using System.Collections.Generic;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.State.Skill;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SkillStateFactory
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillPropertyDto, SkillCode> _skillViewResource;
        private readonly AilmentStateFactory _ailmentStateFactory;
        private readonly BuffStateFactory _buffStateFactory;
        private readonly DamageStateFactory _damageStateFactory;
        private readonly RestoreStateFactory _restoreStateFactory;
        private readonly SlipStateFactory _slipStateFactory;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        
        public SkillStateFactory(
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillPropertyDto, SkillCode> skillViewResource,
            AilmentStateFactory ailmentStateFactory,
            BuffStateFactory buffStateFactory,
            DamageStateFactory damageStateFactory,
            RestoreStateFactory restoreStateFactory,
            SlipStateFactory slipStateFactory,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _container = container;
            _skillFactory = skillFactory;
            _skillViewResource = skillViewResource;
            _ailmentStateFactory = ailmentStateFactory;
            _buffStateFactory = buffStateFactory;
            _damageStateFactory = damageStateFactory;
            _restoreStateFactory = restoreStateFactory;
            _slipStateFactory = slipStateFactory;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public SkillState Create(SkillCode skillCode, IList<CharacterId> targetIdList) => new(
            skillCode: skillCode,
            targetIdList: targetIdList,
            container: _container, 
            skillFactory: _skillFactory,
            skillViewResource: _skillViewResource,
            ailmentStateFactory: _ailmentStateFactory,
            buffStateFactory: _buffStateFactory,
            damageStateFactory: _damageStateFactory,
            restoreStateFactory: _restoreStateFactory,
            slipStateFactory: _slipStateFactory,
            messageView: _messageView,
            playerImageView: _playerImageView);
    }
}
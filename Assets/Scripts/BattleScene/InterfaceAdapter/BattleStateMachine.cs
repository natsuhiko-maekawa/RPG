using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Framework.Input;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.State.Battle;
using VContainer;
using VContainer.Unity;
using Context = BattleScene.InterfaceAdapter.State.Battle.Context;

namespace BattleScene.InterfaceAdapter
{
    public class BattleStateMachine : IStartable
    {
        private Context _context;
        private readonly EnemiesDomainService _enemies;
        private readonly PlayerDomainService _player;
        private readonly BattleSceneInput _battleSceneInput;
        private readonly GridView _gridView;
        private readonly TargetView _targetView;
        private readonly IObjectResolver _container;

        public BattleStateMachine(
            EnemiesDomainService enemies,
            PlayerDomainService player,
            BattleSceneInput battleSceneInput,
            GridView gridView,
            TargetView targetView,
            IObjectResolver container)
        {
            _enemies = enemies;
            _player = player;
            _battleSceneInput = battleSceneInput;
            _gridView = gridView;
            _targetView = targetView;
            _container = container;
        }

        void IStartable.Start()
        {
            _context = new Context(_container.Resolve<InitializeBattleState>());
            _battleSceneInput.SetSelectAction(_context.Select);
            _gridView.SetSelectAction(x => _context.Select(x));
            _targetView.SetSelectAction(x => _context.Select(ToCharacterIdList(x)));
        }

        private ImmutableList<CharacterId> ToCharacterIdList(IList<CharacterDto> characterDtoList)
        {
            return characterDtoList
                .Select(x => x.IsPlayer
                    ? _player.GetId()
                    : _enemies.GetIdByPosition(x.EnemyIndex))
                .ToImmutableList();
        }
    }
}
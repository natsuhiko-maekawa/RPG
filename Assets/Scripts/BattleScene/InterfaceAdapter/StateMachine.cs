using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.IInput;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.State.Battle;
using VContainer;
using VContainer.Unity;

namespace BattleScene.InterfaceAdapter
{
    public class StateMachine : IStartable
    {
        private Context _context;
        private readonly EnemiesDomainService _enemies;
        private readonly PlayerDomainService _player;
        private readonly IBattleSceneInput _battleSceneInput;
        private readonly IGridView _gridView;
        private readonly ITargetView _targetView;
        private readonly IObjectResolver _container;

        public StateMachine(
            EnemiesDomainService enemies,
            PlayerDomainService player,
            IBattleSceneInput battleSceneInput,
            IGridView gridView,
            ITargetView targetView,
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
            _gridView.SetSelectAction(x => _context.Select((ActionCode)x));
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
        
        public void Start()
        {
            _context = new Context(_container.Resolve<InitializeBattleState>());
        }
        
        public void Select()
        {
            _context.Select();
        }
        
        public void SelectAction(int actionCode)
        {
            _context.Select((ActionCode)actionCode);
        }
    }
}
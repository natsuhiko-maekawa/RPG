using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Framework.InputActions;
using BattleScene.Framework.ViewModels;
using BattleScene.InterfaceAdapter.States.Battle;
using VContainer;
using Context = BattleScene.InterfaceAdapter.States.Battle.Context;


namespace BattleScene.InterfaceAdapter.StateMachines
{
    public class BattleStateMachine : IEntryPoint, INoArgumentActions, ISelectRowAction, ISelectTargetAction
    {
        private Context _context = null!;
        private readonly EnemiesDomainService _enemies;
        private readonly PlayerDomainService _player;
        private readonly IObjectResolver _container;

        public BattleStateMachine(
            EnemiesDomainService enemies,
            PlayerDomainService player,
            IObjectResolver container)
        {
            _enemies = enemies;
            _player = player;
            _container = container;
        }

        public void Start()
        {
            _context = new Context(_container.Resolve<InitializeBattleState>());
        }

        public void OnSelect() => _context.Select();
        public void OnSelect(int id) => _context.Select(id);
        public void OnSelect(IReadOnlyList<CharacterModel> targetDtoList) 
            => _context.Select(ToCharacterList(targetDtoList));
        public void OnCancel() => _context.Cancel();

        private IReadOnlyList<CharacterEntity> ToCharacterList(IReadOnlyList<CharacterModel> characterModelList)
        {
            return characterModelList
                .Select(x => x.IsPlayer
                    ? _player.Get()
                    : _enemies.GetByPosition(x.Position))
                .ToArray();
        }
    }
}
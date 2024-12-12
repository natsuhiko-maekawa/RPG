using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace BattleScene.InterfaceAdapter.States.Turn
{
    public abstract class BaseState
    {
        protected Context Context { get; private set; } = null!;

        public void SetContext(Context context)
        {
            Context = context;
        }

        public virtual void Start() { }
        public virtual void Select() { }
        public virtual void Select(int id) { }
        public virtual void Select(IReadOnlyList<CharacterEntity> targetList) { }
    }
}
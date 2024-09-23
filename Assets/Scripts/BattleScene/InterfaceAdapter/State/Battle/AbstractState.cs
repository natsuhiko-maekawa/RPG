using System.Collections.Generic;
using BattleScene.Domain.Id;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public abstract class AbstractState
    {
        protected Context Context { get; private set; }
        
        public void SetContext(Context context)
        {
            Context = context;
        }

        public virtual void Start() { }
        
        public virtual void Select() { }
        
        public virtual void Select(int id) { }
        
        public virtual void Select(IReadOnlyList<CharacterId> targetIdList) { }
    }
}
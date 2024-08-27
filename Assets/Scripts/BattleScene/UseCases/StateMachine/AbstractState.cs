using System.Collections;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.OldId;

namespace BattleScene.UseCases.StateMachine
{
    public abstract class AbstractState
    {
        protected Context Context { get; set; }

        // ReSharper disable once ParameterHidesMember
        public void SetContext(Context context)
        {
            Context = context;
        }

        public virtual void Start()
        {
        }
        
        public virtual void Select()
        {
        }

        public virtual void Select(SkillCode skillCode)
        {
        }

        public virtual void Select(ActionCode actionCode)
        {
        }

        public virtual void Select(CharacterId characterId)
        {
        }

        public virtual void Select(IList<CharacterId> characterIdList)
        {
        }
    }
}
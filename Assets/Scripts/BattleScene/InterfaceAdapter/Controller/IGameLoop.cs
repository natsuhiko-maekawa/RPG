using System;

namespace BattleScene.InterfaceAdapter.Controller
{
    public interface IGameLoop
    {
        public void Subscribe(Action start);
    }
}
using BattleScene.Framework.InputActions;
using UnityEngine;
using VContainer;

namespace BattleScene.Framework
{
    public class EntryPoint : MonoBehaviour
    {
        private IEntryPoint _entryPoint;

        [Inject]
        public void Construct(IEntryPoint entryPoint)
        {
            _entryPoint = entryPoint;
        }

        public void Start()
        {
            _entryPoint.Start();
        }
    }
}
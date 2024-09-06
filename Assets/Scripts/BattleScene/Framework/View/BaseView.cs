using System.Threading.Tasks;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public abstract class BaseView<TDto> : MonoBehaviour
    {
        public abstract Task StartAnimation(TDto dto);
    }
}
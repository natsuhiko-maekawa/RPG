using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.Dto.Interface;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public abstract class BaseView<T> : MonoBehaviour where T : IDto
    {
        public abstract Task StartAnimation(T dto);
    }
}
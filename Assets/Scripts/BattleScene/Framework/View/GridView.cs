using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.GridView;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class GridView : MonoBehaviour, IGridView
    {
        public Task StartAnimationAsync(GridViewDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
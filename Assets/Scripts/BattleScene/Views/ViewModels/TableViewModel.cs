using System.Collections.Generic;
using BattleScene.Views.Code;

namespace BattleScene.Views.ViewModels
{
    public struct TableViewModel
    {
        public ActionCode ActionCode { get; }
        public IReadOnlyList<RowModel> RowList { get; }

        public TableViewModel(
            ActionCode actionCode, 
            IReadOnlyList<RowModel> rowList)
        {
            ActionCode = actionCode;
            RowList = rowList;
        }
    }
}
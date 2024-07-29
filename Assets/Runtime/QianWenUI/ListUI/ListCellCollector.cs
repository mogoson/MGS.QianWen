/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ListCellCollector.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/26
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using MGS.UI.Widget;

namespace MGS.QianWen.UI
{
    public class ListCellCollector : UICollector<ListCell, ListCellOpt>
    {
        public event Action<ListCell, bool> OnToggleEvent;
        public event Action<ListCell> OnCellDeleteEvent;

        public override ListCell CreateCell()
        {
            var cell = base.CreateCell();
            cell.OnToggleEvent += (cell, isOn) => OnToggleEvent?.Invoke(cell, isOn);
            cell.OnDeleteEvent += RemoveCell;
            return cell;
        }

        public override void RemoveCell(ListCell cell)
        {
            base.RemoveCell(cell);
            OnCellDeleteEvent?.Invoke(cell);
        }
    }
}
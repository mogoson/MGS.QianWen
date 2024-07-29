/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ListUI.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/26
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine.UI;

namespace MGS.QianWen.UI
{
    public class ListUI : MGS.UI.Widget.UIWidget
    {
        public event Action<ListCell, bool> OnCellToggleEvent;
        public event Action<ListCell> OnCellDeleteEvent;
        public event Action<ListCell> OnCellAddClickEvent;

        public ScrollRect scrList;
        public ListCellCollector collector;
        public Button btnAdd;

        protected virtual void Awake()
        {
            collector.OnToggleEvent += (cell, isOn) => OnCellToggleEvent?.Invoke(cell, isOn);
            collector.OnCellDeleteEvent += cell => OnCellDeleteEvent?.Invoke(cell);
            btnAdd.onClick.AddListener(OnBtnAddClick);
        }

        protected void OnBtnAddClick()
        {
            var opts = new ListCellOpt() { content = ListCellOpt.DEFAULT_CONTENT };
            AddCell(opts);
        }

        public ListCell AddCell(ListCellOpt opts)
        {
            var cell = collector.CreateCell(opts);
            cell.togContent.isOn = true;

            LayoutRebuilder.ForceRebuildLayoutImmediate(scrList.content);
            scrList.verticalNormalizedPosition = 0;

            OnCellAddClickEvent?.Invoke(cell);
            return cell;
        }

        public void ClearCells()
        {
            collector.Refresh(null);
        }
    }
}
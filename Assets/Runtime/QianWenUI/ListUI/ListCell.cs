/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ListCell.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/26
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using MGS.UI.Widget;
using UnityEngine.UI;

namespace MGS.QianWen.UI
{
    public struct ListCellOpt
    {
        public const string DEFAULT_CONTENT = "...";
        public string content;
    }

    public class ListCell : UIRefreshable<ListCellOpt>
    {
        public event Action<ListCell, bool> OnToggleEvent;
        public event Action<ListCell> OnDeleteEvent;

        public Toggle togContent;
        public Text txtContent;
        public Button btnDelete;

        private void Awake()
        {
            togContent.onValueChanged.AddListener((isOn) => OnToggleEvent?.Invoke(this, isOn));
            btnDelete.onClick.AddListener(() => OnDeleteEvent?.Invoke(this));
        }

        protected override void OnRefresh(ListCellOpt data)
        {
            txtContent.text = data.content;
        }
    }
}
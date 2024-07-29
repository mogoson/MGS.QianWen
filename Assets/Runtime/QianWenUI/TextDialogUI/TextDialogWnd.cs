/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TextDialogWnd.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/27
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using MGS.UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.QianWen.UI
{
    public class TextDialogWnd : UIWidget
    {
        public event Action<TextDialogUI> OnCreateDialogEvent;
        public event Action<TextDialogUI> OnDeleteDialogEvent;

        public RectTransform list;
        public ListUI listUI;

        [Space]
        public RectTransform dialog;
        public TextDialogUI dialogUI;

        [Space]
        public Button btnFlexible;
        public Text texFlexible;

        protected Dictionary<ListCell, TextDialogUI> dialogs = new Dictionary<ListCell, TextDialogUI>();
        protected float listWidth;

        protected virtual void Awake()
        {
            btnFlexible.onClick.AddListener(OnBtnFlexibleClick);
            listUI.OnCellAddClickEvent += ListUI_OnCellAddClickEvent; ;
            listUI.OnCellToggleEvent += ListUI_OnCellClickEvent;
            listUI.OnCellDeleteEvent += ListUI_OnCellDeleteEvent;
            listWidth = listUI.RTransform.rect.width;
        }

        private void ListUI_OnCellDeleteEvent(ListCell cell)
        {
            RemoveDialogUI(cell);
        }

        private void ListUI_OnCellAddClickEvent(ListCell cell)
        {
            var dialog = CreateDialogUI(cell);
            dialogs.Add(cell, dialog);
            ToggleDialogActive(cell, true);
        }

        private void ListUI_OnCellClickEvent(ListCell cell, bool isOn)
        {
            ToggleDialogActive(cell, isOn);
        }

        protected void ToggleDialogActive(ListCell cell, bool isActive)
        {
            if (dialogs.ContainsKey(cell))
            {
                var dialog = dialogs[cell];
                dialog.SetActive(isActive);
                if (!isActive)
                {
                    dialog.AbortQuest();
                }
            }
        }

        protected void OnBtnFlexibleClick()
        {
            list.gameObject.SetActive(!list.gameObject.activeSelf);
            var offsetX = list.gameObject.activeSelf ? listWidth : 0;
            dialog.offsetMin = Vector2.right * offsetX;
            var text = list.gameObject.activeSelf ? "<" : ">";
            texFlexible.text = text;
        }

        protected TextDialogUI CreateDialogUI(ListCell cell)
        {
            var dialog = Instantiate(dialogUI, dialogUI.transform.parent);
            dialog.OnQuest += OnQuest;
            void OnQuest(string question)
            {
                if (cell.Option.content == ListCellOpt.DEFAULT_CONTENT)
                {
                    var data = cell.Option;
                    data.content = question;
                    cell.Refresh(data);
                    dialog.OnQuest -= OnQuest;
                }
            }
            OnCreateDialogEvent?.Invoke(dialog);
            return dialog;
        }

        protected void RemoveDialogUI(ListCell cell)
        {
            var dialog = dialogs[cell];
            if (dialog != null)
            {
                dialog.AbortQuest();
                dialog.Destroy();
            }
            dialogs.Remove(cell);
            OnDeleteDialogEvent?.Invoke(dialog);
        }

        public void CreateDialog(ListCellOpt opt)
        {
            listUI.AddCell(opt);
        }

        public void ClearDialog()
        {
            listUI.ClearCells();
        }
    }
}
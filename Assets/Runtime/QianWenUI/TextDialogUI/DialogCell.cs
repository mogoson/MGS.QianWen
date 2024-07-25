/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DialogCell.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/25
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections;
using MGS.UI.Widget;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MGS.QianWen.UI
{
    public struct DialogCellOpts
    {
        public const string DEFAULT_CONTENT = "...";
        public string content;
        public TextAnchor alignment;
    }

    public class DialogCell : UIRefreshable<DialogCellOpts>
    {
        public event Action<DialogCellOpts> OnHoldEvent;

        public VerticalLayoutGroup layoutGroup;
        public Text txtContent;
        public UIPointerListener listener;
        public float holdOn = 1.0f;
        protected Coroutine holder;

        protected void Awake()
        {
            listener.OnPointerDownEvent += Listener_OnPointerDownEvent;
            listener.OnPointerUpEvent += Listener_OnPointerUpEvent;
        }

        private void Listener_OnPointerUpEvent(PointerEventData obj)
        {
            if (holder != null)
            {
                StopCoroutine(holder);
            }
        }

        private void Listener_OnPointerDownEvent(PointerEventData data)
        {
            IEnumerator Hold()
            {
                yield return new WaitForSeconds(holdOn);
                OnHoldEvent?.Invoke(Option);
                holder = null;
            }
            holder = StartCoroutine(Hold());
        }

        protected override void OnRefresh(DialogCellOpts data)
        {
            layoutGroup.childAlignment = data.alignment;
            txtContent.text = data.content;
        }
    }
}
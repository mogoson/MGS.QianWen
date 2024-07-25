/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TextDialogUI.cs
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
using UnityEngine.UI;

namespace MGS.QianWen.UI
{
    public class TextDialogUI : UIWidget, ITextDialogUI
    {
        public event Action<string> OnQuest;
        public event Action<string> OnHold;
        public event Action OnAbort;

        public ScrollRect scrDialog;
        public DialogCellCollector collector;
        public InputField iptQuestion;
        public Button btnSend;
        public Text txtSend;
        public Color sendColor = Color.green;

        [Space]
        public UIDialog dialogUI;
        public UITooltip tooltipUI;
        public Vector3 tipOffset = new Vector2(-50, 50);

        protected Color originColor;
        protected DialogCell answerCell;
        protected bool IsBusy;
        protected Camera uiCamera;
        protected Coroutine checker;

        protected void Awake()
        {
            btnSend.onClick.AddListener(OnBtnSendClick);
            collector.OnCellHoldEvent += Collector_OnCellHoldEvent;
            originColor = txtSend.color;
            uiCamera = transform.root.GetComponent<Canvas>().worldCamera;
        }

        protected void Collector_OnCellHoldEvent(DialogCellOpts opts)
        {
            OnHold?.Invoke(opts.content);
        }

        protected void OnBtnSendClick()
        {
            AbortQuest();
            if (string.IsNullOrEmpty(iptQuestion.text))
            {
                return;
            }
            StartQuest(iptQuestion.text);
            checker = null;
        }

        public void StartQuest(string question)
        {
            IsBusy = true;
            var opt = new DialogCellOpts()
            {
                content = question,
                alignment = TextAnchor.MiddleRight
            };
            collector.CreateCell(opt);

            opt.content = DialogCellOpts.DEFAULT_CONTENT;
            opt.alignment = TextAnchor.MiddleLeft;
            answerCell = collector.CreateCell(opt);
            scrDialog.verticalNormalizedPosition = 0;
            txtSend.color = sendColor;

            OnQuest?.Invoke(question);
            iptQuestion.text = string.Empty;
        }

        public void OnRespond(string answer)
        {
            if (string.IsNullOrEmpty(answer))
            {
                return;
            }

            var opt = new DialogCellOpts()
            {
                content = answer,
                alignment = TextAnchor.MiddleLeft
            };
            answerCell?.Refresh(opt);
            scrDialog.verticalNormalizedPosition = 0;
        }

        public void OnTip(string tip, float during)
        {
            var sPos = Input.mousePosition + tipOffset;
            tooltipUI.RTransform.SetPosition(uiCamera, sPos, Vector2.zero, TextAnchor.LowerCenter);

            var tOpts = new UITooltipOption() { text = tip };
            tooltipUI.Refresh(tOpts);
            tooltipUI.SetActive();

            IEnumerator DelayClose()
            {
                yield return new WaitForSeconds(during);
                tooltipUI.SetActive(false);
            }
            StartCoroutine(DelayClose());
        }

        public void OnError(Exception error)
        {
            var opts = new UIDialogOption()
            {
                tittle = "Error",
                closeButton = true,
                content = error.Message,
                yesButton = "OK"
            };
            dialogUI.Refresh(opts);
            dialogUI.SetActive();
        }

        public void EndQuest(string answer)
        {
            IsBusy = false;
            OnRespond(answer);
            txtSend.color = originColor;
            answerCell = null;
        }

        public void AbortQuest()
        {
            if (IsBusy)
            {
                OnAbort?.Invoke();
                EndQuest(null);
            }
        }
    }
}
/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IDialogUI.cs
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
    public interface IDialogUI<T> : IUIWidget
    {
        event Action<T> OnQuest;
        event Action<T> OnHold;
        event Action OnAbort;

        void StartQuest(T question);

        void OnRespond(T answer);

        void OnTip(string tip, float during);

        void EndQuest(T answer);
    }
}
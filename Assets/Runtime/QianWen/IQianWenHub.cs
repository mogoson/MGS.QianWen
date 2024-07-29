/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IQianWenHub.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/19
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.QianWen
{
    public interface IQianWenHub
    {
        string AppKey { set; get; }

        ITextDialog CreateTextDialog(int timeOut = 60);

        void RemoveDialog(IDialog dialog);

        void RemoveDialog(string guid);

        void ClearDialogs();
    }
}
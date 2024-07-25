/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  QianWenApi.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/25
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.QianWen
{
    public class QianWenApi
    {
        public TextApi textApi;
    }

    public class TextApi
    {
        public string url;
        public string model;
        public string role;
    }
}